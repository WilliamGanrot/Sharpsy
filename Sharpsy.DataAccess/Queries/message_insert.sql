insert into dbo.[Message]([Text], UserId, RoomId)
OUTPUT INSERTED.MessageId
values(@Text, @UserId, @RoomId)
