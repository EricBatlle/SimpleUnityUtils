<?php
	//Include other php files
	include('DBConst.php');

	//Variables submitted
	$newUser = $_POST["newUser"];
	$newUser = json_decode($newUser, true);

	//Create DB connection
	$conn = new mysqli($GLOBALS['serverName'], $GLOBALS['username'], $GLOBALS['password'], "simple_db");

	//Check connection
	if($conn->connect_error)
	{
		echo WebResponse::$ERROR;
		die("connection failed ".$conn->connect_error);		
	}
	//Create Query to insert new raider recived
	$sqlQuery ="INSERT INTO user (username, password, email) VALUES ";	
	$sqlQuery .= "('".$newUser["username"]."', '".$newUser["password"]."', '".$newUser["email"]."');"; 

	//Validate query	
	if($conn->query($sqlQuery) == TRUE)
	{				
		$newUser["userID"] = $conn->insert_id;
		echo WebResponse::$OK . WebResponse::$SEPARATOR . json_encode($newUser);			
	}
	else
	{
		//echo mysqli_error($conn);
		echo WebResponse::$ERROR_REGISTER_DUPLICATE_USERNAME;
	}
	
	//Close connection with DB
	$conn->close();	
?>

