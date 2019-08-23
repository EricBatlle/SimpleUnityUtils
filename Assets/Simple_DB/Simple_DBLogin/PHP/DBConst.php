<?php
	//Store that file in D/xampp/htdocs/myProjectorWhateverfoldername
	$serverName = "localhost";
	$username = "root"; //usually root
	$password = ""; //usually "" (empty)
	$dbname = "simple_db"; //
	
	define("ERROR_HEADER","ERROR: ");
	//define("SEPARATOR","|");
	class WebResponse
    {		
		public static $SEPARATOR = "|";
		public static $OK = "OK";
        public static $ERROR = ERROR_HEADER."Something went wrong :(";
        public static $ERROR_DBCONNECTION = ERROR_HEADER."Connection with the DB failed";
		public static $ERROR_0RESULTS = ERROR_HEADER."0 Results";
		public static $ERROR_LOGIN_WRONG_CREDENTIALS = ERROR_HEADER."Wrong Credentials";
        public static $ERROR_LOGIN_UNEXISTANT_USERNAME = ERROR_HEADER."Username does not exist";
        public static $ERROR_REGISTER_DUPLICATE_USERNAME = ERROR_HEADER."This username already exists";
    }
	$WebResponse = new WebResponse;
?>