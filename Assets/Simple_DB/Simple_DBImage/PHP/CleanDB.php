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
	
	//Create Query to insert new raider recived
	$sqlQuery ="SET FOREIGN_KEY_CHECKS = 0;";	
	$sqlQuery.="TRUNCATE TABLE image;";
	$sqlQuery.="SET FOREIGN_KEY_CHECKS = 1;";	

	//ejecutar multi consulta
	if ($conn->multi_query($sqlQuery)) {
		do {			
			if ($conn->more_results()) {
				//printf("-----------------\n");
			}
		} while ($conn->next_result());
		echo WebResponse::$OK;		
	}
	else
	{
		echo WebResponse::$ERROR;
	}

	//Close connection with DB
	$conn->close();	
?>