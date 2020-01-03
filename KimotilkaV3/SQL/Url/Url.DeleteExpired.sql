set nocount on
set quoted_identifier, ansi_nulls, ansi_warnings, arithabort, concat_null_yields_null, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec dbo.create_proc
  @ObjectName = 'dbo.[Url.DeleteExpired]'
go

alter procedure dbo.[Url.DeleteExpired]
  --vars--

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

  begin tran TRAN_delete_expired

    delete u from dbo.Url as u
    where u.ExpirationDate is not null
      and u.ExpirationDate <= @now


  commit tran TRAN_delete_expired



end
go
