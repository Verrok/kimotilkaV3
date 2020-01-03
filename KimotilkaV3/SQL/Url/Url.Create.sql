set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec [dbo].create_proc
     @ObjectName = 'dbo.[Url.Create]'
go

alter procedure [dbo].[Url.Create]
  --vars--
  @Hash varchar(max),
  @Url nvarchar(1024),
  @StartDate datetime = null,
  @ExpireDate datetime = null
  --------
  as
  begin
    set nocount on
    set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
    set numeric_roundabort off
    set transaction isolation level read uncommitted
    set xact_abort on

    declare
      @now datetime = getdate(),
      @exp datetime

    select
      @exp = dateadd(year, 2555, @now)

    insert into [dbo].[Url]
      (
        [Hash],
        [Url],
        [CreateDate],
        [ExpirationDate],
        [IsActive]
      )
      values
        (
          @Hash,
          compress(@Url),
          nullif(@StartDate, @now),
          nullif(@ExpireDate, @exp),
          1
        )


  end
go
