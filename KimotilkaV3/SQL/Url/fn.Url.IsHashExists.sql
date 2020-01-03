set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec [dbo].create_func
     @ObjectName = 'dbo.[fn.Url.IsHashExists]'
go

alter function [dbo].[fn.Url.IsHashExists] (@Hash varchar(256))
  returns bit
  as
  begin
    declare
      @IsExist bit

    select
        @IsExist = iif(exists(select u.[Hash]
                                from [dbo].[Url] as [u]
                                where [u].[Hash] = @Hash
                                  and [u].[IsActive] = 1), 1, 0)

    return @IsExist
  end
go


