﻿CREATE TABLE [dbo].[Message]
(
	[MessageId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[UserId]	INT NOT NULL,
	[RoomId]	INT NOT NULL,
	[Sent]		datetime DEFAULT CURRENT_TIMESTAMP,
	[Text]		nvarchar(max) NOT NULL,
	CONSTRAINT [FK_Message_User] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUser]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_Room_Room] FOREIGN KEY ([RoomId]) REFERENCES [Room]([RoomId]) ON DELETE CASCADE
)
