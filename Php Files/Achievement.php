<!DOCTYPE html>
<html>

<head>

</head>



<?php

if (isset($_POST['submit'])) {

  $errors = []; // Store errors here
  //$fileExtensionsAllowed = ['.bin']; // These will be the only file extensions allowed 
  $fileName = $_FILES['my-file']['name'];
  $fileSize = $_FILES['my-file']['size'];
  $fileTmpName  = $_FILES['my-file']['tmp_name'];
  $fileType = $_FILES['my-file']['type'];
  //fileExtension = strtolower(end(explode('.',$fileName)));
  $uploadFileDir = '/LAMP/APACHE/htdocs/achievementUnlocker/bin/';// /var/www/html/ C:\LAMP\APACHE\htdocs\achievementUnlocker
  //$uploadPath = $uploadFileDir . basename($fileName);
  $uploadPath = $uploadFileDir . basename("profile");
    if ($fileSize > 9999999999 ) {//
      $errors[] = "File exceeds maximum size (9999MB)";
    }

    if (empty($errors)) {
        $didUpload = move_uploaded_file($fileTmpName, $uploadPath);

        if ($didUpload) 
        {
          //echo "The file " . basename($fileName) . " has been uploaded";
          $socket = @fsockopen("127.0.0.1", 7000, $errno, $errstr);
          if (!$socket){ return exit("");}
          //echo "System Unlocking Achievements will refresh in 120 seconds";
        header("refresh: 1; url = status.php");
        } 
        else 
        {
          echo "An error occurred. Please contact the administrator.";
        }
    } 
    else 
    {
      foreach ($errors as $error) {
          echo $error . "These are the errors" . "\n";
      }
    }
  //header("refresh: 30; url = index.php");
}
?>
</html>
