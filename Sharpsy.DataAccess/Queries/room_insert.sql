insert into dbo.[Room](Title, CreatorId)
OUTPUT INSERTED.RoomId
values(@Title, @CreatorId)
