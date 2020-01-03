--proc name--
exec dbo.create_proc
  @ObjectName = 'dbo.[Url.Deactivate]'
go

alter procedure dbo.[Url.Deactivate]
  --vars--
  @Hash varchar(256)
  --------
as
begin
  set nocount on
  set quoted_identifier, ansi_nulls, ansi_warnings, arithabort, concat_null_yields_null, ansi_padding on
  set numeric_roundabort off
  set transaction isolation level read uncommitted
  set xact_abort on

  begin tran TRAN_deactivate

    update dbo.[Url] set
      [IsActive] = 0
      where [Hash] = @Hash
        and [IsActive] = 1


  commit tran TRAN_deactivate



end
go
