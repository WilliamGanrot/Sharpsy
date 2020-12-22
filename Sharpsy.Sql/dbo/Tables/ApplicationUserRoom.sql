CREATE TABLE [dbo].[ApplicationUserRoom]
(
	[ApplicationUserRoomId]		INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[UserId]	INT NOT NULL,
	[RoomId]	INT NOT NULL,
	CONSTRAINT [FK_ApplicationUserRoom_User] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUser]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_ApplicationUserRoom_Room] FOREIGN KEY ([RoomId]) REFERENCES [Room]([RoomId]) ON DELETE CASCADE
)
