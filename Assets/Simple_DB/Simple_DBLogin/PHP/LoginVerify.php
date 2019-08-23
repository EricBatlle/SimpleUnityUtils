<?php
	//Include other php files
	include('DBConst.php');

	//Variables submitted
	$newUsername = $_POST["newUsername"];
	$newPassword = $_POST["newPassword"];
	
	//Create DB connection
	$conn = new mysqli($GLOBALS['serverName'], $GLOBALS['username'], $GLOBALS['password'], $GLOBALS['dbname']);

	//Check connection
	if($conn->connect_error)
	{
		echo WebResponse::$ERROR;
		die("connection failed ".$conn->connect_error);		
	}
	
	//Create Query to insert new raider recived
	$sqlQuery ="SELECT * FROM user WHERE username = '".$newUsername."'";	
	$result = $conn->query($sqlQuery);

	if($result->num_rows > 0)
	{
		while($row = $result->fetch_assoc())
		{
			if($row["password"] == $newPassword)
			{				
				echo WebResponse::$OK . WebResponse::$SEPARATOR . json_encode($row);
			}
			else
			{
				echo WebResponse::$ERROR_LOGIN_WRONG_CREDENTIALS;
			}			
		}
	}
	else
	{
		echo WebResponse::$ERROR_LOGIN_UNEXISTANT_USERNAME;
	}

	//Close connection with DB
	$conn->close();	
?>