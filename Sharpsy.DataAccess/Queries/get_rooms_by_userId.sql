WITH test AS (
	SELECT [dbo].[Room].RoomId
	FROM [dbo].[Room]
	INNER JOIN [dbo].[ApplicationUserRoom] on [dbo].[Room].RoomId = [dbo].[ApplicationUserRoom].RoomId
	WHERE [dbo].[ApplicationUserRoom].UserId = @UserId
)

SELECT *
FROM test
INNER JOIN [dbo].[Room] on [dbo].[Room].RoomId = test.RoomId
INNER JOIN [dbo].[ApplicationUserRoom] on [dbo].[ApplicationUserRoom].RoomId = test.RoomId
INNER JOIN [dbo].[ApplicationUser] on [dbo].[ApplicationUser].Id = [dbo].[ApplicationUserRoom].UserId
