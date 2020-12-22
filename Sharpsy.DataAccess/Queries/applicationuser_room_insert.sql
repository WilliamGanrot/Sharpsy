insert into dbo.[ApplicationUserRoom](UserId, RoomId)
OUTPUT INSERTED.Id
values(@UserId, @RoomId)
