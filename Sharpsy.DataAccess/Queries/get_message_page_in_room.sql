SELECT TOP(@Offset) * 
FROM [dbo].[Message]
INNER JOIN [dbo].ApplicationUser ON [dbo].ApplicationUser.Id = [dbo].[Message].UserId
INNER JOIN [dbo].Room ON [dbo].Room.RoomId = [dbo].[Message].RoomId
WHERE [dbo].[Message].RoomId = @RoomId
ORDER BY [dbo].[Message].MessageId
