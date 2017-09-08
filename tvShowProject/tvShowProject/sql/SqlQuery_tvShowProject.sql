use ProjTest
go
create schema
	tv
	go

--drop table
--	tv.[User]
create table
	tv.TvTable
	(
		ID int PRIMARY KEY identity not null,
		IMDB_ID nvarchar(50) UNIQUE not null,
		Title nvarchar (max) null,
		QueryString nvarchar (max) null,
		NextReleaseDate DateTime null
	)

create table
	tv.[User]
	(
		ID int PRIMARY KEY identity not null,
		AspNetUserID nvarchar (450) UNIQUE not null
	)

create table
	tv.UserToTvTable
	(
		UserID int references tv.[user](ID),
		TvTableID int references tv.TvTable(ID),
		PRIMARY KEY (UserID, TvTableID)
	)
--insert into
--	tv.[user](AspNetUserID)
--	select Id from dbo.aspnetusers

use ProjTest

select * from dbo.AspNetUsers
where UserName='Niklas'
go

delete from dbo.AspNetUsers
where UserName='Kalle'
go

select * from tv.[User]
where ID=1003
go

delete from tv.[User]
where ID=1007
go

insert into tv.UserToTvTable(UserID, TvTableID)
	values (1014, 2)
