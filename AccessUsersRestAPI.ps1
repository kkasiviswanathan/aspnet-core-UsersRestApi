
#---------------------------------------------
# Add a new user
#---------------------------------------------
$newUser = @{Firstname="Joseph";Lastname="Turner";Address="737 Edgewood Avenue";City="Fresno";State="CA";Zipcode="93721";Age=39;Interests="Kite surfing"} | ConvertTo-Json
$params = @{
Uri = 'http://localhost:60323/api/Users'
Method = 'POST'
Body = $newUser
ContentType = 'application/json'
}

$userAdded = Invoke-RestMethod @params | ConvertTo-Json


#---------------------------------------------
# Query all users who have 'jo' in their name
#---------------------------------------------

$name = "jo"
$getUsersByNameUrl = "http://localhost:60323/api/Users/GetByName?name=$name"
Invoke-RestMethod -Uri $getUsersByNameUrl

#---------------------------------------------
# Delete the newly added user
#---------------------------------------------

$deletename = "joseph"
$userToDelete = Invoke-WebRequest "http://localhost:60323/api/Users/GetByName?name=$deletename" | ConvertFrom-Json
$idProp = 'id'
$userToDeleteId = $userToDelete.$idProp

Invoke-RestMethod -Uri "http://localhost:60323/api/Users/$userToDeleteId" -Method Delete

Write-Host "Successfully deleted User: Joseph Turner!" -ForegroundColor white -BackgroundColor green

#---------------------------------------------
# Query all users who have 'jo' in their name
#---------------------------------------------

$name = "jo"
$getUsersByNameUrl = "http://localhost:60323/api/Users/GetByName?name=$name"
Invoke-RestMethod -Uri $getUsersByNameUrl