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


    declare
      @sql nvarchar(max)
    set @sql = N''

    select @sql = @sql + 'ALTER TABLE ' + @TableName +
                  + ' DROP CONSTRAINT ['
      + f.name + N']; ' + CHAR(13) + CHAR(10)
    from sys.foreign_keys as f
    where referenced_object_id = object_id(@TableName)



    PRINT @sql
    exec (@sql)


  end
go


