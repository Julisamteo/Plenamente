$(document).ready(function () {
    $('#calendar').fullCalendar({
        locale: 'es',
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Día'
        },
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/Calendario/GetCalendarData',
                type: "GET",
                dataType: "JSON",
                success: function (result) {
                    var events = [];
                    $.each(result, function (i, data) {
                        events.push(
                            {
                                id: data.Id,
                                title: data.Title,
                                description: data.Description,
                                start: data.StartDate,
                                end: data.EndDate,
                                backgroundColor: data.BackgroundColor,
                                borderColor: data.BorderColor,
                                eventClick: function (info) {
                                    location.href = data.EventRoute; 
                                    //alert('Event: ' + info.event.title);
                                    //alert('Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY);
                                    //alert('View: ' + info.view.type);
                                    //// change the border color just for fun
                                    //info.el.style.borderColor = 'red';
                                }
                            });
                    });
                    callback(events);
                }
            });
        },
        eventRender: function (event, element) {
            element.qtip(
                {
                    content: event.description
                });
        },
        editable: false
    });
});