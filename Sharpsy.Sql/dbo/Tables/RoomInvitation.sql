CREATE TABLE [dbo].[RoomInvitation]
(
	[RoomInvitationId]				INT				NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[RoomId]			INT				NOT NULL,
	[SenderUserId]		INT				NOT NULL,
	[Status]			INT				NOT NULL DEFAULT 0,
	[InvitationGUID]	nvarchar(36)	NOT NULL,
	[ReciverEmail]		nvarchar(100)	NOT NULL,
	[Created]			datetime		NOT NULL DEFAULT CURRENT_TIMESTAMP,
	CONSTRAINT [FK_RoomInvitation_Room] FOREIGN KEY ([RoomId]) REFERENCES [Room]([RoomId]) ON DELETE CASCADE,
	CONSTRAINT [FK_RoomInvitation_ApplicationUser] FOREIGN KEY ([SenderUserId]) REFERENCES [ApplicationUser]([Id]) ON DELETE CASCADE,
)
