set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec dbo.create_proc
  @ObjectName = 'dbo.drop_fks'
go

alter procedure dbo.drop_fks
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


  declare @sql nvarchar(max)
  set @sql = N''

  select @sql = @sql + N'ALTER TABLE ' + @TableName
      + N'.' + @TableName
      + N' DROP CONSTRAINT ' -- + QUOTENAME(rc.CONSTRAINT_SCHEMA)  + N'.'  -- not in MS-SQL
      + @TableName + N'; ' + CHAR(13) + CHAR(10)
  from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC

  inner join INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1
      on KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG
      and KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA
      and KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME


  -- PRINT @sql
  exec(@sql)


end
go


