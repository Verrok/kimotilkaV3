set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec dbo.create_proc
  @ObjectName = 'dbo.[Url.CheckHash]'
go

alter procedure dbo.[Url.CheckHash]
  --vars--
  @Hash   varchar(8)
 ,@Exists bit        = null out
  --------
as
begin
  set nocount on
  set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
  set numeric_roundabort off
  set transaction isolation level read uncommitted
  set xact_abort on
  set xact_abort on

  declare
     @true   bit = 1
    ,@false  bit = 0

  select
       @Exists = iif(exists(select
                                 u.Hash
                              from dbo.Url as u
                              where u.Hash = @Hash), @true, @false)



end
go

