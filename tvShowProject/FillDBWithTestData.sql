use ProjTest
go

insert into tv.TvTable(Title, IMDB_ID, QueryString) values('Game of thrones', 'tt0944947', ' ')
insert into tv.TvTable(Title, IMDB_ID, QueryString) values('Breaking bad', 'tt0903747', ' ')
insert into tv.TvTable(Title, IMDB_ID, QueryString) values('Rick and Morty', 'tt2861424', ' ')

insert into tv.UserToTvTable(UserID, TvTableID) values(1, 2)
insert into tv.UserToTvTable(UserID, TvTableID) values(1, 3)