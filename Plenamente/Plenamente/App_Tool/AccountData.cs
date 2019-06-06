using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace Plenamente.App_Tool
{
    /// <summary>
    /// Class that implements the properties to acces de user data.
    /// </summary>
    public static class AccountData
    {
        /// <summary>
        /// The session
        /// </summary>
        private static SessionManager Session = new SessionManager();
        /// <summary>
        /// Gets or sets if has error return true or false in other wase.
        /// </summary>
        /// <value>
        /// If has error
        /// </value>
        private static bool HasError
        {
            get
            {
                try
                {
                    return Session.GetValue<bool>("Session.HasError");
                }
                catch (Exception)
                {
                    return false;
                }
            }
            set => Session.SetValue("Session.HasError", value);
        }
        /// <summary>
        /// Gets a value indicating whether [session in].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [session in]; otherwise, <c>false</c>.
        /// </value>
        public static bool SessionOut => HttpContext.Current == null;
        /// <summary>
        /// Gets or sets the machine ip.
        /// </summary>
        /// <value>
        /// The machine ip.
        /// </value>
        public static string UsuarioId
        {
            get
            {
                string id = Session.GetValue<string>("Session.UserId");
                if (string.IsNullOrEmpty(id))
                {
                    id = HttpContext.Current.User.Identity.GetUserId();
                    Session.SetValue("Session.UserId", id);
                }
                return id;
            }
        }
        /// <summary>
        /// Gets or sets the nit empresa.
        /// </summary>
        /// <value>
        /// The nit empresa.
        /// </value>
        public static int NitEmpresa
        {
            get { return Session.GetValue<int>("Session.NitEmpresa"); }
            set { Session.SetValue("Session.NitEmpresa", value);  }
        }
        /// <summary>
        /// Closes the session.
        /// </summary>
        public static void CloseSession()
        {
            Session.Clear();
        }
    }
    /// <summary>
    /// Class that implements the methods for administrate session
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// Adds the value to persistense variables.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValue(string key, object value)
        {
            if (HttpContext.Current != null)
            {
                System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
                session[key] = value;
            }
        }
        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <typeparam name="T">Data type of return.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>Returns if it [value not equals null] the value of param otherwise default value </returns>
        public T GetValue<T>(string key)
        {
            if (HttpContext.Current != null)
            {
                System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
                return session[key] == null ? default(T) : (T)Convert.ChangeType(session[key], typeof(T));
            }

            return default(T);
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
            session.Clear();
            session.Abandon();
        }
    }
}