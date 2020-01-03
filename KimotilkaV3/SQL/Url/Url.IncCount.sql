--proc name--
exec dbo.create_proc
  @ObjectName = 'dbo.[Url.IncCount]'
go

alter procedure dbo.[Url.IncCount]
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

  begin tran TRAN_FollowCount

    update dbo.[Url] set
      [FollowCount] = [FollowCount] + 1
      where [Hash] = @Hash
        and [IsActive] = 1


  commit tran TRAN_FollowCount



end
go
