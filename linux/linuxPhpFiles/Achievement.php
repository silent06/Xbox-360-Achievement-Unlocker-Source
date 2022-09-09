<!DOCTYPE html>
<html>

<head>

</head>



<?php






  $target_dir = "/var/www/html/achieve/";
  $newFileName = $target_dir .'profile'.'.'. pathinfo($_FILES["my-file"]["name"] ,PATHINFO_EXTENSION); //get the file extension and append it to the new file name
  $uploadOk = 1;
  $imageFileType = pathinfo($_FILES["my-file"]["name"] ,PATHINFO_EXTENSION);
  // Check if image file is a actual image or fake image
  if(isset($_POST["submit"])) {
      //$check = getimagesize($_FILES["fileToUpload"]["tmp_name"]);
      /*if($check !== false) {
          echo "File is an image - " . $check["mime"] . ".";
          $uploadOk = 1;
      } else {
          echo "File is not an image.";
          $uploadOk = 0;
      }*/
      $uploadOk = 1;
  }else {

    $uploadOk = 0;
  }

  // Check if $uploadOk is set to 0 by an error
  if ($uploadOk == 0) {
      echo "Sorry, your file was not uploaded.";
  // if everything is ok, try to upload file
  } else {
      if (move_uploaded_file($_FILES["my-file"]["tmp_name"],  $newFileName)) {
          echo "The file ". basename( $_FILES["my-file"]["name"]). " has been uploaded.";

          $old_path = getcwd();
          chdir('/var/www/html/achieve/');
          $output = shell_exec('/bin/sh script.sh');
          chdir($old_path);
          //echo "<pre>$output</pre>";
          header("refresh: 1; url = status.php");
  
      } else {
          echo "Sorry, there was an error uploading your file.";
      }
  }




//if (isset($_POST['submit'])) {



  //$errors = []; // Store errors here
  //$fileExtensionsAllowed = ['.bin']; // These will be the only file extensions allowed 
  //$fileName = $_FILES['my-file']['name'];
  //$fileSize = $_FILES['my-file']['size'];
  //$fileTmpName  = $_FILES['my-file']['tmp_name'];
  //$fileType = $_FILES['my-file']['type'];
  

   // get details of the uploaded file
   //$fileTmpPath = $_FILES['my-file']['tmp_name'];
   //$fileName = $_FILES['my-file']['name'];
   //$fileSize = $_FILES['my-file']['size'];
   //$fileType = $_FILES['my-file']['type'];
   //$fileNameCmps = explode(".", $fileName);
   //$fileExtension = strtolower(end($fileNameCmps));

   //$allowedfileExtensions = array('jpg', 'gif', 'png', 'zip', 'txt', 'xls', 'doc', 'bin');


   // sanitize file-name
   //$oldFileName = md5(time() . $fileName) . '.' . $fileExtension;

  
   

  // directory in which the uploaded file will be moved
  //$uploadFileDir = '/var/www/html/achieve/bin/';
  //$dest_path = $uploadFileDir . $newFileName;
  //move_uploaded_file($fileTmpName, $uploadPath);
  //fileExtension = strtolower(end(explode('.',$fileName)));
  //$uploadFileDir = '/var/www/html/achieve/bin/';// /var/www/html/achievementunlocker C:\LAMP\APACHE\htdocs\achievementUnlocker
  //$uploadPath = $uploadFileDir . basename($fileName);
  //$uploadPath = $uploadFileDir . basename("profile");
    /*if ($fileSize > 9999999999 ) {//
      $errors[] = "File exceeds maximum size (9999MB)";
    }

    if (empty($errors)) {
        $didUpload = move_uploaded_file($fileTmpPath, $dest_path);
        //move_uploaded_file( $source, $destination );

        if ($didUpload) 
        {
          //echo "The file " . basename($fileName) . " has been uploaded";
          //echo "System Unlocking Achievements will refresh in 120 seconds";
            $old_path = getcwd();
            chdir('/var/www/html/achieve/');
            $output = shell_exec('/bin/sh script.sh');
            chdir($old_path);
            //echo "<pre>$output</pre>";
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
}*/
?>
</html>
