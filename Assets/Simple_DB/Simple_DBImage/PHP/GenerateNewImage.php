<?php
	//Include other php files
	include('DBConst.php');

	//Variables submitted
	$myFile = $_FILES["fileUpload"];

	//Create DB connection
	$conn = new mysqli($GLOBALS['serverName'], $GLOBALS['username'], $GLOBALS['password'], $GLOBALS['dbname']);

	//Check connection
	if($conn->connect_error)
	{
		echo WebResponse::$ERROR;
		die("connection failed ".$conn->connect_error);		
	}
	
	//Get Image byteData
    $imgData = addslashes(file_get_contents($myFile['tmp_name']));

	//Create Query to insert new image recived
	$sqlQuery ="INSERT INTO image (name, imageData) VALUES ";	
	$sqlQuery .= "('".$myFile["name"]."', '".$imgData."');"; 

	//Validate query	
	if($conn->query($sqlQuery) == TRUE)
	{	
		echo WebResponse::$OK;
	}
	else
	{
		echo WebResponse::$ERROR;
	}
	
	//Close connection with DB
	$conn->close();	
?>

