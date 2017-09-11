use ProjTest
go

sp_help 'tv.TvTable'

--drop constraint UQ__TvTable__21608C1854EDC7C9

alter table tv.tvTable drop column IMDB_ID