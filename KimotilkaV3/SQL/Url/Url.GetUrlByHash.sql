go
set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec [dbo].create_proc
     @ObjectName = 'dbo.[Url.GetUrlByHash]'
go

alter procedure [dbo].[Url.GetUrlByHash]
  --vars--
  @Hash varchar(256)
  --------
  as
  begin
    set nocount on
    set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
    set numeric_roundabort off
    set transaction isolation level read uncommitted
    set xact_abort on

    select top 1
      [u].[Hash]                                   as [Hash],
      cast(decompress([u].[Url]) as nvarchar(max)) as [Link],
      [u].[CreateDate]                             as [CreateDate],
      [u].[ExpirationDate]                         as [ExpirationDate],
      [u].[IsActive]                               as [IsActive],
      [u].[FollowCount]                            as [FollowCount]
      from [dbo].[Url] as [u]
      where [u].[Hash] = @Hash
        and [u].[IsActive] = 1

  end
go
