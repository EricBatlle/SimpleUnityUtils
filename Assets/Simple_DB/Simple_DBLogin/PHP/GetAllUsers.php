<?php
	//Include other php files
	include('DBConst.php');

	//Create DB connection
	$conn = new mysqli($GLOBALS['serverName'], $GLOBALS['username'], $GLOBALS['password'], $GLOBALS['dbname']);

	//Check connection
	if($conn->connect_error)
	{
		echo WebResponse::$ERROR;
		die("connection failed ".$conn->connect_error);		
	}
	
	//Create SQL query and link it to the DB
	$sqlQuery = "SELECT * FROM user";
	$result = $conn->query($sqlQuery);
	$json_array = array();

	//Manage Query response
	if($result->num_rows > 0)
	{
		//output data of each row
		while($row = $result->fetch_assoc()) //Like c# dictionary, where the key is the column name
		{
			$json_array[] = $row;
		}	
		echo WebResponse::$OK . WebResponse::$SEPARATOR . json_encode($json_array);
	}
	else
	{
		echo WebResponse::$ERROR_0RESULTS;
	}

	$conn->close();
?>

