insert into dbo.[ApplicationUserRoom](UserId, RoomId)
OUTPUT INSERTED.ApplicationUserRoomId
values(@UserId, @RoomId)
