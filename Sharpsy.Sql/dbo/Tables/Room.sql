CREATE TABLE [dbo].[Room]
(
	[RoomId]	INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Title]		nvarchar(100) NOT NULL,
	[CreatorId]	INT	NOT NULL,
	[Created]	datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[LastUpdated] datetime NOT NULL DEFAULT  CURRENT_TIMESTAMP,
	CONSTRAINT [FK_Room_User] FOREIGN KEY ([CreatorId]) REFERENCES [ApplicationUser]([Id]),
)
