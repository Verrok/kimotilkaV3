set nocount on
set quoted_identifier, ansi_nulls, ansi_warnings, arithabort, concat_null_yields_null, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec dbo.create_proc
  @ObjectName = 'dbo.[Url.DeleteByHash]'
go

alter procedure dbo.[Url.DeleteByHash]
  --vars--
  @Hash varchar(8)
  --------
as
begin
  set nocount on
  set quoted_identifier, ansi_nulls, ansi_warnings, arithabort, concat_null_yields_null, ansi_padding on
  set numeric_roundabort off
  set transaction isolation level read uncommitted
  set xact_abort on

  declare
    @now datetime = cast(cast(dateadd(minute, DateDiff(minute, 0, GetDate()), 0) AS smalldatetime) as datetime)

  begin tran TRAN_delete

    delete from dbo.[Url]
      where Hash = @Hash


  commit tran TRAN_delete



end
go
