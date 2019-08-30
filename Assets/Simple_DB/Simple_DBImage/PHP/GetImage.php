<?php
	//Include other php files
	include('DBConst.php');

	//Variables submitted
	//$dayID = $_POST["dayID"];
	$imageName = "fileUpload.dat";

	//Create DB connection
	$conn = new mysqli($GLOBALS['serverName'], $GLOBALS['username'], $GLOBALS['password'], $GLOBALS['dbname']);

	//Check connection
	if($conn->connect_error)
	{
		echo WebResponse::$ERROR;
		die("connection failed ".$conn->connect_error);		
	}
	
	//Create SQL query and link it to the DB
	$sqlQuery = "SELECT * FROM image";
	$result = $conn->query($sqlQuery);

	//Manage Query response
	if($result->num_rows > 0)
	{	
		$row = mysqli_fetch_array($result);			
		echo WebResponse::$OK . WebResponse::$SEPARATOR . base64_encode($row["imageData"]);
	}
	else
	{
		echo mysqli_error($conn);
		echo WebResponse::$ERROR_0RESULTS;
	}

	$conn->close();
?>

