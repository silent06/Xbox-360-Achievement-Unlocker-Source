<!DOCTYPE html>
<html lang="en" >
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Xbox 360 Achievement Unlocker - Dank as FUck!</title>
  <link rel="stylesheet" href="./style.css">
  <link href="css/bootstrap.css" rel="stylesheet" media="screen">
  <link href="css/bootstrap.min.css" rel="stylesheet" media="screen">
  <link href="http://twitter.github.com/bootstrap/assets/css/bootstrap.css" rel="stylesheet">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
  <script src="http://twitter.github.com/bootstrap/assets/js/bootstrap-tooltip.js"></script>
  <script src="http://twitter.github.com/bootstrap/assets/js/bootstrap-popover.js"></script>
  <script src="https://code.jquery.com/jquery.js"></script>
  <script src="js/bootstrap.min.js"></script>
  <script src="https://code.jquery.com/jquery.js"></script>
  <script src="js/bootstrap.min.js"></script>
  <script>
  function scroll() {
    $("#console").animate({scrollTop:$("#console")[0].scrollHeight - $("#console").height()}, 0, function() { })
  }
  </script>
</head>

<body onload="scroll()">
<p style="background-image: url('DBZ.jpg');">
<h1>Achievement Unlocker!</h1>
<form action="Achievement.php" method="post" enctype="multipart/form-data">
  <div class="input-file-container">  
    <input class="input-file" name="my-file" id="my-file" type="file">
    <label tabindex="0" for="my-file" class="input-file-trigger">Select a file...</label>
    <input type="submit" class="btn btn-primary" name="submit" value="Unlock Achievements">
    <p class="file-return"></p>
  </div>
</form>
<div class="container">
  <div class="row">
        <div class="col-lg-12">
            <div class="bs-component">
                <textarea id="console" style="margin: 0px; width: 1200px; height: 750px;"readonly>
                <?php
                    echo file_get_contents("profile.log"); 
                ?></textarea>
                <a href="index.php" class="btn btn-primary">Refresh</a>
            </div>
        </div>
    </div>
</div>


<p class="txtcenter copy">by <a href="DBZ.jpg">@Silent#1917</a><br/>see also <a href="https://silentlive.gq">This</a></p>
<!-- partial -->
<script src="./script.js"></script>
<style>
    body {
    background-image: url('DBZ.jpg');
    }
</style> 
</body>
</html>
