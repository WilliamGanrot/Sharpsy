SELECT *
FROM [dbo].ApplicationUserRoom
WHERE [dbo].ApplicationUserRoom.RoomId = @RoomId
and [dbo].ApplicationUserRoom.UserId = @UserId