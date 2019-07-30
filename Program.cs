using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.Security;
using System;
using UpdateMyEFPassword.Logger;

namespace UpdateMyEFPassword
{
    class Program
    {
	 string userName = string.Empty; 
	 string userPassword = string.Empty; 

	 static void Main(string[] args)
        {
            ELKLogger.RegisterDependencies();
            ELKLogger.Init(ApplicationLogger.GetEnvironmentVariable(), "NLog.config", "elklog");
            #region For Password-Update
            try
            {
                ApplicationLogger.LogActionTrackerInfo("Application started");
                string connectionString = ConfigurationManager.ConnectionStrings["EFCOMLoungeConnectionString"].ConnectionString;
				string connString = ConfigurationManager.ConnectionStrings["EFCOMLoungeConnectionString"].ConnectionString;
                UpdatePasswordDataContext upd = new UpdatePasswordDataContext(connString);
                List<GetUsersToUpdatePasswordTempResult> resultObj = upd.GetUsersToUpdatePasswordTemp().ToList();
                string userPassword = null, userName = null;
                foreach (GetUsersToUpdatePasswordTempResult tObj in resultObj)
                {
                    try
                    {

                        userPassword = string.Empty;
                        userName = string.Empty;
						MembershipUser mu = Membership.GetUser(tObj.MembershipId);
                        if (mu != null)
                        {
                            if (mu.IsLockedOut)
                            {
                                mu.UnlockUser();
                            }
                            userName = mu.UserName;
                            userPassword = mu.GetPassword();

                            //Update the record with the password.
                            upd.UpdatePasswordByUserIdTemp(tObj.User_Id, userName, userPassword);
                        } else
                        {
                            throw new Exception(String.Format("User ID {0} and membership {1} doesn't exist. Process failed", tObj.User_Id, tObj.MembershipId));
                        }

                    }
                    catch(Exception ex)
                    {
                        Dictionary<string, object> attributes = new Dictionary<string, object>();
                        attributes.Add(ApplicationLogger.methodName, ApplicationLogger.GetMyMethodName());
                        ApplicationLogger.LogException(ex, attributes);
                    }
                }
            }
            catch (Exception ex)
            {
                Dictionary<string, object> attributes = new Dictionary<string, object>();
                attributes.Add(ApplicationLogger.methodName, ApplicationLogger.GetMyMethodName());
                ApplicationLogger.LogException(ex, attributes);
            }
            #endregion
        }
    }
}
