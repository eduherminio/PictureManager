using System;

namespace PictureManager.Api.Logs
{
    public static class LogHelpers
    {
        public static string ValueToLog(object objectToLog)
        {
            string stringifiedObject;
            try
            {
                stringifiedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToLog);
            }
            catch (PlatformNotSupportedException)
            {
                stringifiedObject = "Non serializable value";
            }
            catch (Exception)
            {
                stringifiedObject = "";
            }

            return stringifiedObject;
        }
    }
}
