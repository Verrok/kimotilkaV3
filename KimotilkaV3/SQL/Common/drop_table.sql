set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec dbo.create_proc
  @ObjectName = 'dbo.drop_table'
go

alter procedure dbo.drop_table
  --vars--
  @TableName sysname
  --------
as
begin
  set nocount on
  set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
  set numeric_roundabort off
  set transaction isolation level read uncommitted
  set xact_abort on

  declare
    @sql  nvarchar(4000) = 'drop table if exists ' + @TableName + ';'

  exec dbo.drop_fks
    @TableName = @TableName


  exec(@sql)
end
go

