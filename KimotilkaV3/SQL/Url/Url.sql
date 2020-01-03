go
set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

exec dbo.drop_table
  @TableName = 'dbo.[Url]'


create table dbo.[Url] (
   [ID]             int        identity(1,1) not null
  ,[Hash]           varchar(8)               not null
  ,[Url]            varchar(512)             not null
  ,[CreateDate]     datetime                 not null
  ,[ExpirationDate] datetime                     null
  ,constraint [Url.pk] primary key clustered (ID, Hash, Url)
)

create unique index [Url.uqHash]
on [dbo].[Url](
   Hash
)
go