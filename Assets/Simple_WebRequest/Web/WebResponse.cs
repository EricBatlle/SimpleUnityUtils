using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// WebResponse manage if the responses from WebRequests are correct or no
/// </summary>
public static class WebResponse
{
    // !!! WARNING !!!
    // This is just an example of possible error messages, you have to match them to your backend web code!!!
    // !!! WARNING !!!
    private static string ERROR_HEADER = "ERROR: ";
    private static string SEPARATOR = "|";

    public static string OK = "OK";
    public static string ERROR = ERROR_HEADER + "Something went wrong :(";
    public static string ERROR_DBCONNECTION = ERROR_HEADER + "Connection with the DB failed";
    public static string ERROR_0RESULTS = ERROR_HEADER + "0 Results";
    public static string ERROR_LOGIN_WRONG_CREDENTIALS = ERROR_HEADER + "Wrong Credentials";
    public static string ERROR_LOGIN_UNEXISTANT_USERNAME = ERROR_HEADER + "Username does not exist";
    public static string ERROR_REGISTER_DUPLICATE_USERNAME = ERROR_HEADER + "This username already exists";    
    public static string ERROR_RESPONSE_HAVE_NO_EXTRAINFO = "There is no extra info on the response";

    //Returns the extra content (the information aside of the web response, like json elements, etc)
    public static string GetResponseInfo(string result)
    {
        return result.Contains(SEPARATOR) ? result.Split(new[] { SEPARATOR }, StringSplitOptions.None).Last() : ERROR_RESPONSE_HAVE_NO_EXTRAINFO;
    }

    //Compare the response with the result, true if they match
    public static bool isEqualTo(string webResponse, string result)
    {
        string resultResponsePart = "result";
        if (result.Contains(SEPARATOR))
            resultResponsePart = result.Split(new[] { SEPARATOR },StringSplitOptions.None).First(); //This new[] is needed as split only takes char or string[] as param
        else
            resultResponsePart = result;

        //It's necessary the Trim(), as spaces could difer depending where and how you handle the petitions
        if (webResponse.Trim().Equals(resultResponsePart.Trim(), StringComparison.InvariantCultureIgnoreCase))
            return true;
        else
            return false;
    }

    //The same as WebResponse.isEqualTo(WebResponse.OK, result), but as it is usually needed, is the way to indicate that "everything works"
    public static bool isResultOk(string result)
    {
        if (isEqualTo(WebResponse.OK, result))
            return true;
        else
        {
            if (!result.Contains(ERROR_HEADER))
            {
                return true;
            }
        }
        return false;
    }        
}