﻿UPDATE [dbo].[RoomInvitation]
SET [dbo].[RoomInvitation].[Status] = @Status
WHERE [dbo].[RoomInvitation].InvitationId = @InvitationId;