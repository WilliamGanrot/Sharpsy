UPDATE [dbo].[RoomInvitation]
SET [dbo].[RoomInvitation].[Status] = 3
WHERE [dbo].[RoomInvitation].[Status] = 0 /*No answer*/
and [dbo].[RoomInvitation].Created < DATEADD(hh, -48, GETDATE())