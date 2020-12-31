SELECT * 
FROM [dbo].[RoomInvitation]
INNER JOIN [dbo].[Room] on [dbo].[Room].RoomId = [dbo].[RoomInvitation].RoomId
INNER JOIN [dbo].ApplicationUser on [dbo].[ApplicationUser].Id = [dbo].[RoomInvitation].SenderUserId
WHERE [dbo].[RoomInvitation].InvitationGUID = @InvitationGUID