SELECT * 
FROM [dbo].[Room]
INNER JOIN [dbo].[ApplicationUserRoom] on [dbo].[Room].RoomId = [dbo].[ApplicationUserRoom].RoomId
WHERE [dbo].[ApplicationUserRoom].UserId = @UserId