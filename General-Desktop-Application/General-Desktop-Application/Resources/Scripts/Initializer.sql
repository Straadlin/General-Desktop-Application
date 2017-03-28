


CREATE DATABASE _dbname_;

GO

USE _dbname_;

GO

CREATE LOGIN _internal_ WITH PASSWORD = N'_pspsps_', DEFAULT_DATABASE = [_dbname_], DEFAULT_LANGUAGE = [Spanish], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF; CREATE USER _internal_ FOR LOGIN [_internal_]; ALTER ROLE [db_datareader] ADD MEMBER [_internal_]; ALTER ROLE [db_datawriter] ADD MEMBER [_internal_];

GO
CREATE SCHEMA build_level000;
GO
CREATE SCHEMA build_level001;
GO
CREATE SCHEMA build_level002;
GO
CREATE SCHEMA build_level003;
GO
CREATE SCHEMA build_level004;

GO

CREATE TABLE build_level001.[state] (stat_uuid__uniqueidentifier uniqueidentifier NOT NULL, stat_name__varchar varchar(100) NOT NULL, coun_uuid__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (stat_uuid__uniqueidentifier));
CREATE TABLE build_level001.country (coun_uuid__uniqueidentifier uniqueidentifier NOT NULL, coun_code__varchar varchar(6) NOT NULL, coun_name__varchar varchar(100) NOT NULL, PRIMARY KEY (coun_uuid__uniqueidentifier));
CREATE TABLE build_level001.[user] (user_uuid__uniqueidentifier uniqueidentifier NOT NULL, user_username__varchar varchar(100) NULL, user_email__varchar varchar(100) NULL, user_cellphone__varchar varchar(10) NULL, user_password__varbinary varbinary(max) NOT NULL, user_firstname__varbinary varbinary(max) NOT NULL, user_lastname__varbinary varbinary(max) NOT NULL, user_roleaccess__tinyint tinyint NOT NULL, user_extradata__varchar varchar(max) NULL, reso_uuid_picture__uniqueidentifier uniqueidentifier NULL, date_uuid_birthdate__uniqueidentifier uniqueidentifier NULL, city_uuid__uniqueidentifier uniqueidentifier NULL, sess_uuid_used__uniqueidentifier uniqueidentifier NULL, sess_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, user_uuid_root__uniqueidentifier uniqueidentifier NULL, sess_uuid_deleted__uniqueidentifier uniqueidentifier NULL, PRIMARY KEY (user_uuid__uniqueidentifier));
CREATE TABLE build_level001.city (city_uuid__uniqueidentifier uniqueidentifier NOT NULL, city_name__varchar varchar(100) NOT NULL, stat_uuid__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (city_uuid__uniqueidentifier));
CREATE TABLE build_level002.[session] (sess_uuid__uniqueidentifier uniqueidentifier NOT NULL, sess_starttime__time time(0) NOT NULL, sess_lastactivity__time time(0) NOT NULL, sess_ipbatch01__tinyint tinyint NULL, sess_ipbatch02__tinyint tinyint NULL, sess_ipbatch03__tinyint tinyint NULL, sess_ipbatch04__tinyint tinyint NULL, sess_extradata__varchar varchar(max) NULL, date_uuid__uniqueidentifier uniqueidentifier NOT NULL, user_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (sess_uuid__uniqueidentifier));
CREATE TABLE build_level002.[date] (date_uuid__uniqueidentifier uniqueidentifier NOT NULL, date_value__date date NOT NULL, PRIMARY KEY (date_uuid__uniqueidentifier));
CREATE TABLE build_level002.[resource] (reso_uuid__uniqueidentifier uniqueidentifier NOT NULL, reso_name__varchar varchar(100) NOT NULL, reso_extension__tinyint tinyint NOT NULL, reso_description__varchar varchar(max) NULL, reso_value__varbinary varbinary(max) NULL, reso_externalurlorname__varchar varchar(max) NULL, PRIMARY KEY (reso_uuid__uniqueidentifier));
CREATE TABLE build_level001.preference (pref_uuid__uniqueidentifier uniqueidentifier NOT NULL, pref_type__int int NOT NULL, pref_value__bit bit NULL, pref_value__tinyint tinyint NULL, pref_value__int int NULL, pref_value__varchar varchar(max) NULL, user_uuid__uniqueidentifier uniqueidentifier NOT NULL, sess_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, pref_uuid_root__uniqueidentifier uniqueidentifier NULL, PRIMARY KEY (pref_uuid__uniqueidentifier));
CREATE TABLE build_level003.principalcompany (prco_uuid__uniqueidentifier uniqueidentifier NOT NULL, prco_rfc__varchar varchar(13) NULL, prco_name__varchar varchar(100) NOT NULL, prco_address__varchar varchar(255) NULL, prco_phone__varchar varchar(100) NULL, prco_email__varchar varchar(100) NULL, prco_facebook__varchar varchar(100) NULL, prco_developmentmode__bit bit NOT NULL, prco_timebetweenbackups__timestamp timestamp NULL, reso_uuid_logo__uniqueidentifier uniqueidentifier NULL, city_uuid__uniqueidentifier uniqueidentifier NULL, sess_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, prco_uuid_root__uniqueidentifier uniqueidentifier NULL, PRIMARY KEY (prco_uuid__uniqueidentifier));
CREATE TABLE build_level003.[version] (vers_uuid__uniqueidentifier uniqueidentifier NOT NULL, vers_name__varchar varchar(12) NOT NULL, date_uuid__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (vers_uuid__uniqueidentifier));

GO

CREATE PROCEDURE build_level000.proc_systemserver_getdatetime
	@dateTime VARCHAR OUTPUT
	AS
		SELECT CONVERT(VARCHAR(24), GETDATE(), 120) 
--	RETURN 1

GO

CREATE FUNCTION build_level000.func_systemserver_generatehashpassword(
	@value VARCHAR(MAX))
	RETURNS VARBINARY(MAX)
	AS
	BEGIN 
		DECLARE @result VARBINARY(MAX)

		SELECT @result = HASHBYTES('sha2_512', HASHBYTES('sha2_256', @value))

		RETURN @result
	END;

GO

--CREATE PROCEDURE build_level000.proc_systemserver_comparepassword
--	@user_uuid__uniqueidentifier UNIQUEIDENTIFIER,
--	@password VARCHAR(MAX)
--	AS
--		DECLARE @generatePassword VARBINARY(MAX)

--		DECLARE @ok BIT	
--		SET @ok = 0

--		SELECT @generatePassword = build_level000.func_systemserver_generatehashpassword(@password)

--		SELECT @ok = 1 FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier AND user_password__varbinary = @generatePassword)

--		SELECT @ok

GO

--SELECT * FROM build_level001.[user]
--EXEC build_level000.proc_systemserver_verifyloginuser 'admin', 'admin'
--SELECT * FROM build_level001.[user] WHERE(user_username__varchar = 'admin' AND user_uuid_root__uniqueidentifier IS NULL AND sess_uuid_deleted__uniqueidentifier IS NULL)

GO

CREATE PROCEDURE build_level000.proc_systemserver_verifyloginuser
	@userNameEmailOrCellphone VARCHAR(MAX),
	@password VARCHAR(MAX)
	AS
		DECLARE @generatePassword VARBINARY(MAX)
		SELECT @generatePassword = build_level000.func_systemserver_generatehashpassword(@password)

		DECLARE @userUUID UNIQUEIDENTIFIER	
		SET @userUUID = NULL
		SELECT @userUUID = user_uuid__uniqueidentifier FROM build_level001.[user] WHERE((user_username__varchar = @userNameEmailOrCellphone OR user_email__varchar = @userNameEmailOrCellphone OR user_cellphone__varchar = @userNameEmailOrCellphone) AND user_password__varbinary = @generatePassword AND user_uuid_root__uniqueidentifier IS NULL AND sess_uuid_deleted__uniqueidentifier IS NULL)
		SELECT @userUUID AS userUUID

		--DECLARE @generatePassword VARBINARY(MAX)
		--SELECT @generatePassword = build_level000.func_systemserver_generatehashpassword(@password)

		--DECLARE @ok BIT	
		--SET @ok = 0
		--SELECT @ok = 1 FROM build_level001.[user] WHERE((user_username__varchar = @userNameEmailOrCellphone OR user_email__varchar = @userNameEmailOrCellphone OR user_cellphone__varchar = @userNameEmailOrCellphone) AND user_password__varbinary = @generatePassword AND user_uuid_root__uniqueidentifier IS NULL AND sess_uuid_deleted__uniqueidentifier IS NULL)
		--SELECT @ok

GO

CREATE FUNCTION build_level000.func_systemserver_encrypt(
	@value VARCHAR(MAX))
	RETURNS VARBINARY(MAX)
	AS
	BEGIN 
		DECLARE @password VARCHAR(MAX)
		SET @password = 'd-f5]8T.x6_[s3'
		
		DECLARE @result VARBINARY(MAX)
		SELECT @result = ENCRYPTBYPASSPHRASE(@password, @value);
		RETURN @result
	END;

GO

CREATE FUNCTION build_level000.func_systemserver_decrypt(
	@value VARBINARY(MAX))
	RETURNS VARCHAR(MAX)
	AS
	BEGIN 
		DECLARE @password VARCHAR(MAX)
		SET @password = 'd-f5]8T.x6_[s3'

		DECLARE @result VARCHAR(MAX)
		SELECT @result = CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE(@password, @value));
		RETURN @result
	END;

GO

--CREATE PROCEDURE build_level000.proc_systemserver_getdatetime
--	@dateTime DATETIME OUTPUT
--	AS
--		SELECT @dateTime = GETDATE()
--	RETURN 1

--SELECT CONVERT(VARCHAR(24),GETDATE(),113) 
--DECLARE @dateTime VARCHAR
--EXEC build_level000.proc_systemserver_getdatetime @dateTime OUTPUT;
--SELECT @dateTime

--GO

CREATE PROCEDURE build_level001.proc_user_insert 
	@user_uuid__uniqueidentifier uniqueidentifier, 
	@user_username__varchar varchar(100), 
	@user_email__varchar varchar(100), 
	@user_cellphone__varchar varchar(10), 
	@user_password__varchar varchar(max), 
	@user_firstname__varchar varchar(max), 
	@user_lastname__varchar varchar(max), 
	@user_roleaccess__tinyint tinyint, 
	@user_extradata__varchar varchar(max), 
	@reso_uuid_picture__uniqueidentifier uniqueidentifier, 
	@date_uuid_birthdate__uniqueidentifier uniqueidentifier, 
	@city_uuid__uniqueidentifier uniqueidentifier, 
	@sess_uuid_used__uniqueidentifier uniqueidentifier, 
	@sess_uuid_created__uniqueidentifier uniqueidentifier, 
	@user_uuid_root__uniqueidentifier uniqueidentifier, 
	@sess_uuid_deleted__uniqueidentifier uniqueidentifier
	AS
		BEGIN TRY
			--IF((SELECT COUNT(*) FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier)) > 0)
			--BEGIN
			--	RETURN 0
			--END

			BEGIN TRAN tranOperation
				INSERT INTO build_level001.[user](
					user_uuid__uniqueidentifier, 
					user_username__varchar, 
					user_email__varchar, 
					user_cellphone__varchar, 
					user_password__varbinary, 
					user_firstname__varbinary, 
					user_lastname__varbinary, 
					user_roleaccess__tinyint, 
					user_extradata__varchar, 
					reso_uuid_picture__uniqueidentifier, 
					date_uuid_birthdate__uniqueidentifier, 
					city_uuid__uniqueidentifier, 
					sess_uuid_used__uniqueidentifier, 
					sess_uuid_created__uniqueidentifier, 
					user_uuid_root__uniqueidentifier, 
					sess_uuid_deleted__uniqueidentifier) 
				VALUES(
					@user_uuid__uniqueidentifier, 
					@user_username__varchar, 
					@user_email__varchar, 
					@user_cellphone__varchar, 
					build_level000.func_systemserver_generatehashpassword(@user_password__varchar),
					build_level000.func_systemserver_encrypt(@user_firstname__varchar),
					build_level000.func_systemserver_encrypt(@user_lastname__varchar),
					--HASHBYTES('sha2_512', HASHBYTES('sha2_256', @user_password__varbinary)),
					--ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', @user_firstname__varchar), 
					--ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', @user_lastname__varchar), 
					@user_roleaccess__tinyint, 
					@user_extradata__varchar, 
					@reso_uuid_picture__uniqueidentifier, 
					@date_uuid_birthdate__uniqueidentifier, 
					@city_uuid__uniqueidentifier, 
					@sess_uuid_used__uniqueidentifier, 
					@sess_uuid_created__uniqueidentifier, 
					@user_uuid_root__uniqueidentifier, 
					@sess_uuid_deleted__uniqueidentifier
				);
			COMMIT TRAN tranOperation

			RETURN 1

		END TRY

		BEGIN CATCH
			ROLLBACK TRAN tranOperation
		END CATCH

GO

--CREATE PROCEDURE build_level000.proc_systemserver_encrypt
--	@password VARCHAR(MAX)
--	AS
--	SELECT build_level000.func_systemserver_generatehashpassword(@password)

--EXEC build_level000.proc_systemserver_comparepassword 'A1637AF8-4BD3-4821-A1E3-94C24469C626', 'admN'
--SELECT * FROM build_level001.[user];

--GO

--CREATE FUNCTION build_level000.func_systemserver_encrypt(
--	@value VARCHAR(MAX)
--	)
--	RETURNS VARCHAR(MAX)
--	AS
--	BEGIN 
--		DECLARE @password VARCHAR(MAX)
--		DECLARE @result VARCHAR(MAX)

--		SELECT @result = ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', @value);

--		RETURN @result
--	END;

--GO

--SELECT HASHBYTES('sha2_512', HASHBYTES('sha2_256', 'admin'))
--SELECT build_level000.func_systemserver_generatehashpassword('admin')
--DECLARE @varbinaryField varbinary(max);
--SET @varbinaryField = 0x21232F297A57A5A743894A0E4A801FC3;

--SELECT CONVERT(varchar(max),@varbinaryField,2), 
--@varbinaryField

--SELECT CONVERT(VARCHAR(MAX), user_password__varbinary) FROM build_level001.[user]
--SELECT build_level000.func_systemserver_encrypt((SELECT u.user_firstname__varchar FROM build_level001.[user] AS u WHERE(u.user_uuid__uniqueidentifier = 'A1637AF8-4BD3-4821-A1E3-94C24469C626')), 0);
--EXEC build_level001.proc_user_select 'A1637AF8-4BD3-4821-A1E3-94C24469C626'
--SELECT * FROM build_level001.[user]

GO

CREATE PROCEDURE build_level001.proc_user_select
	@user_uuid__uniqueidentifier UNIQUEIDENTIFIER
	AS
		SELECT
			user_uuid__uniqueidentifier, 
			user_username__varchar, 
			user_email__varchar, 
			user_cellphone__varchar, 
			user_password__varbinary, 
			--user_firstname__varbinary, 
			--user_lastname__varbinary, 
			--CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3',user_firstname__varbinary)) AS user_firstname__varbinary, --It works
			--CAST(DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', user_lastname__varbinary) AS VARCHAR(MAX)) AS user_lastname__varbinary, --It works 
			build_level000.func_systemserver_decrypt(user_firstname__varbinary) AS user_firstname__varchar, 
			build_level000.func_systemserver_decrypt(user_lastname__varbinary) AS user_lastname__varchar, 
			user_roleaccess__tinyint, 
			user_extradata__varchar, 
			reso_uuid_picture__uniqueidentifier, 
			date_uuid_birthdate__uniqueidentifier, 
			city_uuid__uniqueidentifier, 
			sess_uuid_used__uniqueidentifier, 
			sess_uuid_created__uniqueidentifier, 
			user_uuid_root__uniqueidentifier, 
			sess_uuid_deleted__uniqueidentifier
		FROM [build_level001].[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier AND user_uuid_root__uniqueidentifier IS NULL AND sess_uuid_deleted__uniqueidentifier IS NULL);

GO

CREATE PROCEDURE build_level001.proc_user_selectAll
	AS
		SELECT
			user_uuid__uniqueidentifier, 
			user_username__varchar, 
			user_email__varchar, 
			user_cellphone__varchar, 
			user_password__varbinary, 
			--user_firstname__varbinary, 
			--user_lastname__varbinary, 
			--CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3',user_firstname__varbinary)) AS user_firstname__varbinary, --It works
			--CAST(DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', user_lastname__varbinary) AS VARCHAR(MAX)) AS user_lastname__varbinary, --It works 
			build_level000.func_systemserver_decrypt(user_firstname__varbinary) AS user_firstname__varchar, 
			build_level000.func_systemserver_decrypt(user_lastname__varbinary) AS user_lastname__varchar, 
			user_roleaccess__tinyint, 
			user_extradata__varchar, 
			reso_uuid_picture__uniqueidentifier, 
			date_uuid_birthdate__uniqueidentifier, 
			city_uuid__uniqueidentifier, 
			sess_uuid_used__uniqueidentifier, 
			sess_uuid_created__uniqueidentifier, 
			user_uuid_root__uniqueidentifier, 
			sess_uuid_deleted__uniqueidentifier
		FROM [build_level001].[user] WHERE(user_uuid_root__uniqueidentifier IS NULL AND sess_uuid_deleted__uniqueidentifier IS NULL);

GO

--SELECT * FROM build_level001.[user];
--EXEC build_level001.proc_user_select '38819277-0CCC-4551-8A77-68A08E85769C';
--EXEC build_level001.proc_user_select '38819277-0CCC-4551-8B77-68A08E85769C';
--EXEC build_level001.proc_user_insert 
--	@user_uuid__uniqueidentifier = '38819277-0CCC-4551-8B77-68A08E85769C', 
--	@user_username__varchar = 'other', 
--	@user_email__varchar = 'other@other.mx', 
--	@user_cellphone__varchar = '0011223344', 
--	@user_password__varchar = 'other', 
--	@user_firstname__varchar = 'other first name', 
--	@user_lastname__varchar = 'other lastname', 
--	@user_roleaccess__tinyint = 1, 
--	@user_extradata__varchar = NULL, 
--	@reso_uuid_picture__uniqueidentifier = NULL, 
--	@date_uuid_birthdate__uniqueidentifier = NULL, 
--	@city_uuid__uniqueidentifier = NULL, 
--	@sess_uuid_used__uniqueidentifier = NULL, 
--	@sess_uuid_created__uniqueidentifier = 'D6E5AB9D-2B08-43B1-BDFA-5128BC37AF44', 
--	@user_uuid_root__uniqueidentifier = NULL, 
--	@sess_uuid_deleted__uniqueidentifier = NULL;
--SELECT ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')
--SELECT CAST(DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', '') AS VARCHAR(MAX)) 
--DECLARE @psw BYTE(MAX)
--SELECT @psw = ENCRYPTBYPASSPHRASE('KurroSoftware', 'MyPassword')
--SELECT @psw
--SELECT CAST(DECRYPTBYPASSPHRASE('KurroSoftware', ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')) AS VARCHAR(MAX))
--SELECT CAST(DECRYPTBYPASSPHRASE('KurroSoftware', '0x010000003F3EE002D407783EC8B013CB1DA517DBC174056729A517B22CBA8DD70A3AD00C') AS VARCHAR(MAX))
--SELECT DECRYPTBYPASSPHRASE('KurroSoftware', '0x010000003F3EE002D407783EC8B013CB1DA517DBC174056729A517B22CBA8DD70A3AD00C')

--SELECT * FROM [build_level001].[user]
--EXEC build_level001.proc_user_select '60B20288-4476-45EA-8089-5EAA29C551BA'

--DROP PROCEDURE build_level000.proc_systemserver_decrypt
--	@value VARCHAR(MAX),
--	@password VARCHAR(MAX)
--	AS
--	SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE(@password, @value)) AS result;


--SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3','USER')) AS user_firstname__varchar
--SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3','USER')) AS user_firstname__varchar
--SELECT ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')
--SELECT ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')

--EXEC build_level000.proc_systemserver_encryptdecrypt 'Pass', 'Hola', 1
--EXEC build_level000.proc_systemserver_encryptdecrypt 'KurroSoftware', '0x01000000FD9603A6A6BA4BFB1CA27CD42ED8E006FC04ABEFECFB65C0E76001D126DA4040', 0

--CREATE PROCEDURE build_level000.proc_systemserver_encryptdecrypt
--	@password VARCHAR(MAX),
--	@value VARCHAR(MAX),
--	@encrypt BIT
--	AS
--	DECLARE @result VARCHAR(MAX)

--	IF(@encrypt = 1)
--		BEGIN			
--			SELECT @result = ENCRYPTBYPASSPHRASE(@password, @value);
--		END
--	ELSE
--		BEGIN
--			SELECT @result = CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE(@password, @value));
--		END

--	SELECT @result

--GO


--CREATE PROCEDURE build_level000.proc_systemserver_encrypt 
--	@value VARCHAR(MAX)
--	AS
--		--SELECT HASHBYTES('sha2_512', HASHBYTES('sha2_256', @value))
--		SELECT CONVERT(VARCHAR(MAX), HASHBYTES('sha2_512', HASHBYTES('sha2_256', 'admin')))

--GO

--EXEC build_level000.proc_systemserver_encrypt 'Hola'
--EXEC build_level000.proc_systemserver_encrypt 'Hola1'
--SELECT user_password__varbinary FROM build_level001.[user]

--GO

--CREATE FUNCTION build_level000.func_systemserver_getdatetime01()
--	RETURNS DATETIME
--	AS
--	BEGIN 
--		DECLARE @datetime DATETIME
--		SELECT @datetime = GETDATE()
--		RETURN @datetime
--	END;

--GO

--CREATE FUNCTION build_level000.func_systemserver_getdatetime02()
--	RETURNS INT
--	AS
--	BEGIN 
--		RETURN 1
--	END;

--select build_level000.func_systemserver_getdatetime();
--SELECT GETDATE()

--GO

CREATE PROCEDURE build_level001.proc_user_findLastBranch
	@user_uuid__uniqueidentifier______Current uniqueidentifier,
	@user_uuid__uniqueidentifier______Last uniqueidentifier OUTPUT
	AS
		WITH EmpCTE ([user_uuid__uniqueidentifier], [user_username__varchar], [user_email__varchar], [user_cellphone__varchar], [user_uuid_root__uniqueidentifier])
		AS
		(
			SELECT [user_uuid__uniqueidentifier], [user_username__varchar], [user_email__varchar], [user_cellphone__varchar], [user_uuid_root__uniqueidentifier]
				FROM build_level001.[user]
				WHERE [user_uuid__uniqueidentifier] = @user_uuid__uniqueidentifier______Current
			UNION ALL
		--Recursiveness
			SELECT e.[user_uuid__uniqueidentifier], e.[user_username__varchar], e.[user_email__varchar], e.[user_cellphone__varchar], e.[user_uuid_root__uniqueidentifier]
				FROM build_level001.[user] AS e JOIN EmpCTE AS m
					ON e.user_uuid_root__uniqueidentifier = m.[user_uuid__uniqueidentifier]
		)
		SELECT @user_uuid__uniqueidentifier______Last = user_uuid__uniqueidentifier FROM EmpCTE

	RETURN 1

GO

CREATE TRIGGER trigger_user_update
ON build_level001.[user]
INSTEAD OF UPDATE
AS
	BEGIN
		SET NOCOUNT ON;--It's to prevent an assigned instruction returns a result

		IF((SELECT COUNT(*) FROM Inserted WHERE(sess_uuid_used__uniqueidentifier IS NULL)) = 1)
			BEGIN

				--> Here starts part 1

				-- We'll see if in the new values there isn't a value for the used column, and then, we'll see if the previous value was different to
				-- NULL, in this case, we'll refresh this field with NULL value

				DECLARE @user_uuid__uniqueidentifier___Part1 UNIQUEIDENTIFIER
				--DECLARE @band BIT

				SELECT @user_uuid__uniqueidentifier___Part1 = user_uuid__uniqueidentifier--, @band = 1
					FROM Inserted WHERE(sess_uuid_used__uniqueidentifier IS NULL);

				--IF(@band = 1 AND (SELECT COUNT(*) FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @@user_uuid__uniqueidentifier___Part1 AND sess_uuid_used__uniqueidentifier IS NOT NULL)) > 0)
				IF(@user_uuid__uniqueidentifier___Part1 IS NOT NULL AND (SELECT COUNT(*) FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier___Part1 AND sess_uuid_used__uniqueidentifier IS NOT NULL)) > 0)
					BEGIN
						UPDATE build_level001.[user] SET 
							sess_uuid_used__uniqueidentifier = NULL 
							WHERE (user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier___Part1);

					--< Here ends part 1

					END
				ELSE
					BEGIN

						--> Here start part 2

						-- In this point, it'll mean that we'll insert a historic value to be storage, with all its column values.

						BEGIN TRY
							BEGIN TRAN tranOperation

								DECLARE @user_uuid__uniqueidentifier___New uniqueidentifier
								DECLARE @user_username__varchar varchar(100)
								DECLARE @user_email__varchar varchar(100)
								DECLARE @user_cellphone__varchar varchar(10)
								DECLARE @user_password__varbinary varbinary(max)
								DECLARE @user_firstname__varbinary varbinary(max)
								DECLARE @user_lastname__varbinary varbinary(max)
								DECLARE @user_roleaccess__tinyint tinyint
								DECLARE @user_extradata__varchar varchar(max)
								DECLARE @reso_uuid_picture__uniqueidentifier uniqueidentifier
								DECLARE @date_uuid_birthdate__uniqueidentifier uniqueidentifier
								DECLARE @city_uuid__uniqueidentifier uniqueidentifier
								--DECLARE @sess_uuid_used__uniqueidentifier uniqueidentifier
								DECLARE @sess_uuid_created__uniqueidentifier uniqueidentifier
								--DECLARE @user_uuid_root__uniqueidentifier uniqueidentifier
								DECLARE @sess_uuid_deleted__uniqueidentifier uniqueidentifier



								-- We get a new UUID for the copy register
								SET @user_uuid__uniqueidentifier___New = NEWID()
								WHILE((SELECT COUNT(*) FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier___New)) > 0)
								BEGIN
									SET @user_uuid__uniqueidentifier___New = NEWID()
								END



								DECLARE @user_uuid__uniqueidentifier______Current uniqueidentifier
								SELECT @user_uuid__uniqueidentifier______Current = user_uuid__uniqueidentifier FROM inserted



								SELECT TOP 1
									--@user_uuid__uniqueidentifier_New = user_uuid__uniqueidentifier, 
									@user_username__varchar = user_username__varchar, 
									@user_email__varchar = user_email__varchar, 
									@user_cellphone__varchar = user_cellphone__varchar, 
									@user_password__varbinary = user_password__varbinary, 
									@user_firstname__varbinary = user_firstname__varbinary, 
									@user_lastname__varbinary = user_lastname__varbinary, 
									@user_roleaccess__tinyint = user_roleaccess__tinyint, 
									@user_extradata__varchar = user_extradata__varchar, 
									@reso_uuid_picture__uniqueidentifier = reso_uuid_picture__uniqueidentifier, 
									@date_uuid_birthdate__uniqueidentifier = date_uuid_birthdate__uniqueidentifier, 
									@city_uuid__uniqueidentifier = city_uuid__uniqueidentifier, 
									--sess_uuid_used__uniqueidentifier 
									@sess_uuid_created__uniqueidentifier = sess_uuid_created__uniqueidentifier, 
									--user_uuid_root__uniqueidentifier
									@sess_uuid_deleted__uniqueidentifier = sess_uuid_deleted__uniqueidentifier
									FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier______Current);



								--SELECT @user_uuid__uniqueidentifier



								-- We get last ID of the list (Recursiveness)
								DECLARE @user_uuid__uniqueidentifier______Last uniqueidentifier
								EXEC build_level001.proc_user_findLastBranch @user_uuid__uniqueidentifier______Current = @user_uuid__uniqueidentifier______Current,  @user_uuid__uniqueidentifier______Last = @user_uuid__uniqueidentifier______Last OUTPUT;
				


								-- First we store the copy register
								INSERT INTO build_level001.[user](user_uuid__uniqueidentifier, user_username__varchar, user_email__varchar, user_cellphone__varchar, user_password__varbinary, user_firstname__varbinary, user_lastname__varbinary, user_roleaccess__tinyint, user_extradata__varchar, reso_uuid_picture__uniqueidentifier, date_uuid_birthdate__uniqueidentifier, city_uuid__uniqueidentifier, sess_uuid_used__uniqueidentifier, sess_uuid_created__uniqueidentifier, user_uuid_root__uniqueidentifier, sess_uuid_deleted__uniqueidentifier) 
									VALUES(
										@user_uuid__uniqueidentifier___New,
										@user_username__varchar,
										@user_email__varchar,
										@user_cellphone__varchar, 
										@user_password__varbinary, 
										@user_firstname__varbinary, 
										@user_lastname__varbinary, 
										@user_roleaccess__tinyint, 
										@user_extradata__varchar, 
										@reso_uuid_picture__uniqueidentifier, 
										@date_uuid_birthdate__uniqueidentifier, 
										@city_uuid__uniqueidentifier, 
										NULL, 
										@sess_uuid_created__uniqueidentifier, 
										@user_uuid__uniqueidentifier______Last, 
										@sess_uuid_deleted__uniqueidentifier
										);



								SELECT 
									--@user_uuid__uniqueidentifier_New = user_uuid__uniqueidentifier, 
									@user_username__varchar = user_username__varchar, 
									@user_email__varchar = user_email__varchar, 
									@user_cellphone__varchar = user_cellphone__varchar, 
									@user_password__varbinary = user_password__varbinary, 
									@user_firstname__varbinary = user_firstname__varbinary, 
									@user_lastname__varbinary = user_lastname__varbinary, 
									@user_roleaccess__tinyint = user_roleaccess__tinyint, 
									@user_extradata__varchar = user_extradata__varchar, 
									@reso_uuid_picture__uniqueidentifier = reso_uuid_picture__uniqueidentifier, 
									@date_uuid_birthdate__uniqueidentifier = date_uuid_birthdate__uniqueidentifier, 
									@city_uuid__uniqueidentifier = city_uuid__uniqueidentifier, 
									--sess_uuid_used__uniqueidentifier, 
									@sess_uuid_created__uniqueidentifier = sess_uuid_created__uniqueidentifier,
									--user_uuid_root__uniqueidentifier 
									@sess_uuid_deleted__uniqueidentifier = sess_uuid_deleted__uniqueidentifier
									FROM inserted



								UPDATE build_level001.[user] SET  
									user_username__varchar = @user_username__varchar, 
									user_email__varchar = @user_email__varchar, 
									user_cellphone__varchar = @user_cellphone__varchar, 
									user_password__varbinary = @user_password__varbinary, 
									user_firstname__varbinary = @user_firstname__varbinary, 
									user_lastname__varbinary = @user_lastname__varbinary, 
									user_roleaccess__tinyint = @user_roleaccess__tinyint, 
									user_extradata__varchar = @user_extradata__varchar, 
									reso_uuid_picture__uniqueidentifier = @reso_uuid_picture__uniqueidentifier, 
									date_uuid_birthdate__uniqueidentifier = @date_uuid_birthdate__uniqueidentifier, 
									city_uuid__uniqueidentifier = @city_uuid__uniqueidentifier, 
									sess_uuid_used__uniqueidentifier = NULL, 
									sess_uuid_created__uniqueidentifier = @sess_uuid_created__uniqueidentifier, 
									user_uuid_root__uniqueidentifier = NULL, 
									sess_uuid_deleted__uniqueidentifier = @sess_uuid_deleted__uniqueidentifier
									WHERE([user].user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier______Current)


								--
							COMMIT TRAN tranOperation
						END TRY

						BEGIN CATCH
							ROLLBACK TRAN tranOperation
						END CATCH

						--< Here ends part 2
					END
			END
		ELSE
			BEGIN
				--> Here ends part 3

				-- If the register receives one value, different to NULL, in the used column then, we'll only add the value for this column, the others values will be ignored.

				DECLARE  @user_uuid__uniqueidentifier UNIQUEIDENTIFIER
				DECLARE  @sess_uuid_used__uniqueidentifier UNIQUEIDENTIFIER

				SELECT @user_uuid__uniqueidentifier = user_uuid__uniqueidentifier, 
					@sess_uuid_used__uniqueidentifier = sess_uuid_used__uniqueidentifier
					FROM Inserted;

				UPDATE build_level001.[user] SET 
					sess_uuid_used__uniqueidentifier = @sess_uuid_used__uniqueidentifier 
					WHERE (user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier);

				--< Here ends part 3
			END
	END

GO

--UPDATE build_level001.[user] SET user_email__varchar = 'HOLA3' WHERE(user_uuid__uniqueidentifier = '8B319333-003F-4363-ADD6-043C6FAA320D');
--SELECT * FROM build_level001.[user];

--DROP TRIGGER [build_level001].[trigger_user_edit]

--GO

CREATE TRIGGER trigger_country_delete
ON build_level001.country
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_state_delete
ON build_level001.[state]
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_city_delete
ON build_level001.city
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_user_delete
ON build_level001.[user]
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_preference_delete
ON build_level001.preference
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_session_delete
ON build_level002.[session]
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_date_delete
ON build_level002.[date]
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_resource_delete
ON build_level002.[resource]
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_principalcompany_delete
ON build_level003.principalcompany
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

CREATE TRIGGER trigger_version_delete
ON build_level003.[version]
INSTEAD OF DELETE
AS
	BEGIN
		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
	END

GO

DECLARE @countryMexicoUUID uniqueidentifier
DECLARE @stateDurangoUUID uniqueidentifier
DECLARE @stateCoahuilaUUID uniqueidentifier
DECLARE @cityGomezPalacioUUID uniqueidentifier
DECLARE @cityTorreonUUID uniqueidentifier
DECLARE @dateUUID uniqueidentifier
DECLARE @sessionStraadUUID uniqueidentifier
DECLARE @user001UUID uniqueidentifier

SET @countryMexicoUUID = NEWID()
SET @stateDurangoUUID = NEWID()
SET @stateCoahuilaUUID = NEWID()
SET @cityGomezPalacioUUID = NEWID()
SET @cityTorreonUUID = NEWID()
SET @dateUUID = NEWID()
SET @sessionStraadUUID = NEWID()
SET @user001UUID = NEWID()

INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AF', 'Afganist�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AL', 'Albania');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DE', 'Alemania');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AD', 'Andorra');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AO', 'Angola');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AI', 'Anguila');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AQ', 'Ant�rtida');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AG', 'Antigua y Barbuda');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SA', 'Arabia Saudita');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DZ', 'Argelia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AR', 'Argentina');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AM', 'Armenia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AW', 'Aruba');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AU', 'Australia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AT', 'Austria');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AZ', 'Azerbaiy�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BS', 'Bahamas');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BD', 'Banglad�s');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BB', 'Barbados');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BH', 'Bar�in');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BE', 'B�lgica');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BZ', 'Belice');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BJ', 'Ben�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BM', 'Bermudas');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BY', 'Bielorrusia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BO', 'Bolivia, Estado Plurinacional de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BQ', 'Bonaire, San Eustaquio y Saba');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BA', 'Bosnia y Herzegovina');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BW', 'Botsuana');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BR', 'Brasil');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BN', 'Brun�i Darussalam');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BG', 'Bulgaria');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BF', 'Burkina Faso');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BI', 'Burundi');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BT', 'But�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CV', 'Cabo Verde');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KH', 'Camboya');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CM', 'Camer�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CA', 'Canad�');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TD', 'Chad');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CL', 'Chile');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CN', 'China, Rep�blica Popular');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CY', 'Chipre');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CO', 'Colombia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KM', 'Comoras');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CD', 'Congo, La Rep�blica Democr�tica del');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CG', 'Congo');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KR', 'Corea, Rep�blica de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KP', 'Corea, Rep�blica Democr�tica Popular de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CI', 'Costa de Marfil');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CR', 'Costa Rica');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HR', 'Croacia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CU', 'Cuba');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CW', 'Curazao');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DK', 'Dinamarca');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DM', 'Dominica');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EC', 'Ecuador');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EG', 'Egipto');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SV', 'El Salvador');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AE', 'Emiratos �rabes Unidos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ER', 'Eritrea');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SK', 'Eslovaquia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SI', 'Eslovenia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ES', 'Espa�a');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'US', 'Estados Unidos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EE', 'Estonia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ET', 'Etiop�a');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RU', 'Federacion Rusa');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PH', 'Filipinas');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FI', 'Finlandia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FJ', 'Fiyi');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FR', 'Francia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GA', 'Gab�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GM', 'Gambia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GE', 'Georgia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GH', 'Ghana');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GI', 'Gibraltar');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GD', 'Granada');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GR', 'Grecia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GL', 'Groenlandia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GP', 'Guadalupe');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GU', 'Guam');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GT', 'Guatemala');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GF', 'Guayana Francesa');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GG', 'Guernsey');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GW', 'Guinea-Bis�u');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GQ', 'Guinea Ecuatorial');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GN', 'Guinea');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GY', 'Guyana');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HT', 'Hait�');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HN', 'Honduras');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HK', 'Hong Kong');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HU', 'Hungr�a');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IN', 'India');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ID', 'Indonesia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IQ', 'Irak');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IR', 'Ir�n, Rep�blica Isl�mica de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IE', 'Irlanda');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BV', 'Isla Bouvet');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IM', 'Isla de Man');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CX', 'Isla de Navidad');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NF', 'Isla Norfolk');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IS', 'Islandia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AX', 'Islas �land');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KY', 'Islas Caim�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CC', 'Islas Cocos (Keeling)');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CK', 'Islas Cook');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FK', 'Islas Falkland (Malvinas)');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FO', 'Islas Feroe');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GS', 'Islas Georgias del Sur y Sandwich del Sur');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HM', 'Islas Heard y Mcdonald');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MP', 'Islas Marianas del Norte');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MH', 'Islas Marshall');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SB', 'Islas Salom�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TC', 'Islas Turcas y Caicos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UM', 'Islas Ultramarinas Menores de Estados Unidos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VG', 'Islas Virgenes Brit�nicas');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VI', 'Islas Virgenes de Los Estados Unidos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IL', 'Israel');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IT', 'Italia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JM', 'Jamaica');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JP', 'Jap�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JE', 'Jersey');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JO', 'Jordania');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KZ', 'Kazajist�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KE', 'Kenia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KG', 'Kirguist�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KI', 'Kiribati');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KW', 'Kuwait');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LS', 'Lesoto');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LV', 'Letonia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LB', 'L�bano');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LR', 'Liberia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LY', 'Libia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LI', 'Liechtenstein');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LT', 'Lituania');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LU', 'Luxemburgo');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MO', 'Macao');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MK', 'Macedonia, La Antigua Rep�blica Yugoslava de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MG', 'Madagascar');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MY', 'Malasia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MW', 'Malaui');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MV', 'Maldivas');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ML', 'Mal�');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MT', 'Malta');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MA', 'Marruecos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MQ', 'Martinica');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MU', 'Mauricio');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MR', 'Mauritania');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'YT', 'Mayotte');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(@countryMexicoUUID, 'MX', 'M�xico');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FM', 'Micronesia, Estados Federados de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MD', 'Moldavia, Rep�blica de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MC', 'M�naco');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MN', 'Mongolia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ME', 'Montenegro');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MS', 'Montserrat');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MZ', 'Mozambique');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MM', 'Myanmar');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NA', 'Nabimia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NR', 'Nauru');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NP', 'Nepal');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NI', 'Nicaragua');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NG', 'Nigeria');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NE', 'N�ger');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NU', 'Niue');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NO', 'Noruega');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NC', 'Nueva Caledonia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NZ', 'Nueva Zelanda');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'OM', 'Om�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NL', 'Pa�ses Bajos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PK', 'Pakist�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PW', 'Palaos');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PS', 'Palestina');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PA', 'Panam�');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PG', 'Pap�a Nueva Guinea');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PY', 'Paraguay');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PE', 'Per�');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PN', 'Pitcairn');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PF', 'Polinesia Francesa');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PL', 'Polonia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PT', 'Portugal');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PR', 'Puerto Rico');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'QA', 'Qatar');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GB', 'Reino Unido');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CF', 'Rep�blica Centroafricana');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CZ', 'Rep�blica Checa');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LA', 'Rep�blica Democr�tica Popular Lao');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DO', 'Rep�blica Dominicana');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RE', 'Reuni�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RW', 'Ruanda');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RO', 'Rumania');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EH', 'Sahara Occidental');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AS', 'Samoa Americana');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'WS', 'Samoa');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BL', 'San Bartolom�');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KN', 'San Crist�bal y Nieves');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SM', 'San Marino');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MF', 'San Mart�n (Parte Francesa)');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PM', 'San Pedro y Miquel�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VC', 'San Vicente y Las Granadinas');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SH', 'Santa Helena, Ascensi�n y Trist�n de Acu�a');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LC', 'Santa Luc�a');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VA', 'Santa Sede (Ciudad Estado Vaticano)');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ST', 'Santo Tom� y Principe');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SN', 'Senegal');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RS', 'Serbia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SC', 'Seychelles');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SL', 'Sierra Leona');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SG', 'Singapur');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SX', 'Sint Maarten (Parte Neerlandesa)');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SY', 'Siria, Rep�blica Arabe de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SO', 'Somalia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LK', 'Sri Lanka');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SZ', 'Suazilandia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ZA', 'Sud�frica');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SS', 'Sud�n del Sur');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SD', 'Sud�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SE', 'Suecia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CH', 'Suiza');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SR', 'Surinam');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SJ', 'Svalbard y Jan Mayen');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TH', 'Tailandia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TW', 'Taiw�n, Provincia de China');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TZ', 'Tanzania, Rep�blica Unida de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TJ', 'Tayikist�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IO', 'Territorio Brit�nico del Oc�ano �ndico');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TF', 'Territorios Australes Franceses');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TL', 'Timor-Leste');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TG', 'Togo');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TK', 'Tokelau');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TO', 'Tonga');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TT', 'Trinidad y Tobago');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TN', 'T�nez');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TM', 'Turkmenist�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TR', 'Turqu�a');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TV', 'Tuvalu');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UA', 'Ucrania');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UG', 'Uganda');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UY', 'Uruguay');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UZ', 'Uzbekist�n');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VU', 'Vanuatu');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VE', 'Venezuela, Rep�blica Bolivariana de');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VN', 'Viet Nam');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'WF', 'Wallis y Futuna');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'YE', 'Yemen');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DJ', 'Yibuti');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ZM', 'Zambia');
INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ZW', 'Zimbabue');

INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Aguascalientes', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Baja California', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Baja California Sur', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Campeche', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Chiapas', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Chihuahua', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(@stateCoahuilaUUID, 'Coahuila', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Colima', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Distrito Federal', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(@stateDurangoUUID, 'Durango', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Estado de M�xico', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Guanajuato', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Guerrero', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Hidalgo', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Jalisco', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Michoac�n', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Morelos', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Nayarit', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Nuevo Le�n', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Oaxaca', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Puebla', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Quer�taro', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Quintana Roo', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'San Luis Potos�', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Sinaloa', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Sonora', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Tabasco', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Tamaulipas', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Tlaxcala', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Veracruz', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Yucat�n', @countryMexicoUUID);
INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Zacatecas', @countryMexicoUUID);

INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Canatl�n', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Canelas', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Coneto de Comonfort', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Cuencam�', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Durango', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'El Oro', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(@cityGomezPalacioUUID, 'G�mez Palacio', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Gral. Sim�n Bo�var', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Guadalupe Victoria', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Guanacev�', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Hidalgo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ind�', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Lerdo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Mapim�', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Mezquital', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nazas', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nombre de Dios', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nuevo Ideal', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ocampo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ot�ez', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'P�nuco de Coronado', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Pe��n Blanco', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Poanas', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Pueblo Nuevo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Rodeo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Bernardo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Dimas', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Juan de Guadalupe', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Juan del R�o', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Luis del Cordero', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Pedro del Gallo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Santa Clara', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Santiago Papasquiaro', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'S�chil', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Tamazula', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Tepehuanes', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Tlahualilo', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Topia', @stateDurangoUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Vicente Guerrero', @stateDurangoUUID);

INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Abasolo', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Acu�a', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Allende', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Arteaga', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Candela', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Casta�os', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Cuatroci�negas', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Escobedo', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Francisco I. Madero', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Frontera', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'General Cepeda', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Guerrero', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Hidalgo', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Jim�nez', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ju�rez', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Lamadrid', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Matamoros', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Monclova', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Morelos', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'M�zquiz', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nadadores', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nava', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ocampo', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Parras', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Piedras Negras', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Progreso', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ramos Arizpe', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Sabinas', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Sacramento', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Saltillo', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Buenaventura', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Juan de Sabinas', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Pedro', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Sierra Mojada', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(@cityTorreonUUID, 'Torre�n', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Viesca', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Villa Uni�n', @stateCoahuilaUUID);
INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Zaragoza', @stateCoahuilaUUID);


--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'AGUASCALIENTES')
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'BAJA CALIFORNIA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'BAJA CALIFORNIA SUR');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'CAMPECHE');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'CHIAPAS');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'CHIHUAHUA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'COAHUILA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'COLIMA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'DISTRITO FEDERAL');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'DURANGO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'GUANAJUATO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'GUERRERO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'HIDALGO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'JALISCO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'M�XICO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'MICHOAC�N');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'MORELOS');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'NAYARIT');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'NUEVO LE�N');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'OAXACA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'PUEBLA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'QUER�TARO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'QUINTANA ROO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'SAN LUIS POTOS�');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'SINALOA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'SONORA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'TABASCO');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'TAMAULIPAS');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'TLAXCALA');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'VERACRUZ');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'YUCAT�N');
--INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'ZACATECAS');
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUASCALIENTES',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ASIENTOS',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CALVILLO',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COS�O',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JES�S MAR�A',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PABELL�N DE ARTEAGA',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RINC�N DE ROMOS',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO DE LOS ROMO',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEZAL�',  1);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ENSENADA',  2);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MEXICALI',  2);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAYAS DE ROSARITO',  2);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RODOLFO S�NCHEZ T. MANEADERO',  2);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FELIPE',  2);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECATE',  2);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIJUANA',  2);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CABO SAN LUCAS',  3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD CONSTITUCI�N',  3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUERRERO NEGRO', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA MULEG�', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA PAZ', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LORETO', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO ADOLFO L�PEZ MATEOS', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN IGNACIO', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JOS� DEL CABO', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TODOS SANTOS', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA ALBERTO ANDR�S ALVARADO AR�MBURO', 3);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CALKINI', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CANDELARIA', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHAMPOT�N', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DEL CARMEN', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESC�RCEGA', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HECELCHAK�N', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HOPELCH�N', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('POMUCH', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SABANCUY', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO DE CAMPECHE', 4);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACALA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARRIAGA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CACAHOAT�N', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIAPA DE CORZO', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CINTALAPA DE FIGUEROA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMIT�N DE DOM�NGUEZ', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUIXTLA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JIQUIPILAS', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS MARGARITAS', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS ROSAS', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAPASTEPEC', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOTOZINTLA DE MENDOZA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOSINGO', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOZOCOAUTLA DE ESPINOSA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PALENQUE', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PICHUCALCO', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PIJIJIAPAN', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO MADERO SAN BENITO', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('REFORMA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN CRIST�BAL DE LAS CASAS', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAPACHULA DE C�RDOVA Y ORD��EZ', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TONAL�', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXTLA GUTI�RREZ', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VENUSTIANO CARRANZA', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLAFLORES', 5);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BACH�NIVA', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIHUAHUA', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COLONIA AN�HUAC', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUHT�MOC', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('DELICIAS', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HIDALGO DEL PARRAL', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOS� MARIANO JIM�NEZ', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN ALDAMA', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JU�REZ', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MADERA', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MANUEL OJINAGA', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVO CASAS GRANDES', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA ROSAL�A DE CAMARGO', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAUCILLO', 6);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALLENDE', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARTEAGA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CASTA�OS', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD ACU�A', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MELCHOR MUZQUIZ', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUATRO CI�NEGAS DE CARRANZA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRANCISCO I. MADERO CH�VEZ', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRONTERA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MATAMOROS', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MONCLOVA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MORELOS', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NADADORES', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAVA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVA ROSITA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARRAS DE LA FUENTE', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PIEDRAS NEGRAS', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RAMOS ARIZPE', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SABINAS', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALTILLO', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BUENAVENTURA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TORRE�N', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VIESCA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZARAGOZA', 7);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DE ARMER�A', 8);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DE VILLA DE �LVAREZ', 8);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COLIMA', 8);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MANZANILLO', 8);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECOMAN', 8);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('�LVARO OBREG�N', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AZCAPOTZALCO', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BENITO JU�REZ', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COYOAC�N', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAJIMALPA DE MORELOS', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUHT�MOC', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUSTAVO A. MADERO', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IZTACALCO', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IZTAPALAPA', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA MAGDALENA CONTRERAS', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MIGUEL HIDALGO', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MILPA ALTA', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TL�HUAC', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLALPAN', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VENUSTIANO CARRANZA', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XOCHIMILCO', 9);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CANATL�N', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD LERDO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL SALTO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRANCISCO I. MADERO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('G�MEZ PALACIO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NOMBRE DE DIOS', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PE��N BLANCO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN DEL R�O DEL CENTAURO DEL NORTE', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MAR�A DEL ORO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO PAPASQUIARO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VICENTE GUERRERO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VICTORIA DE DURANGO', 10);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ABASOLO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AC�MBARO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APASEO EL ALTO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APASEO EL GRANDE', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CELAYA', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MANUEL DOBLADO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMONFORT', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CORTAZAR', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUER�MARO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('DOCTOR MORA', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EMPALME ESCOBEDO ESCOBEDO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUANAJUATO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAN�MARO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IRAPUATO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JARAL DEL PROGRESO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JER�CUARO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LE�N DE LOS ALDAMA', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARFIL', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOROLE�N', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('P�NJAMO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUR�SIMA DE BUSTOS', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RINC�N DE TAMAYO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ROMITA', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALAMANCA', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALVATIERRA', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN DIEGO DE LA UNI�N', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO DEL RINC�N', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JOS� ITURBIDE', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS DE LA PAZ', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MIGUEL DE ALLENDE', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA CRUZ JUVENTINO ROSAS', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO MARAVAT�O', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SILAO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TARANDACUAO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('URIANGATO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE DE SANTIAGO', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLAGR�N', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YURIRIA', 11);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACAPULCO DE JU�REZ', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARCELIA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATOYAC DE �LVAREZ', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AYUTLA DE LOS LIBRES', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AZOY�', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BUENAVISTA DE CUELLAR', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHILAPA DE �LVAREZ', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHILPANCINGO DE LOS BRAVO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD ALTAMIRANO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD APAXTLA DE CASTREJ�N', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COPALA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COYUCA DE BEN�TEZ', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COYUCA DE CATAL�N', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CRUZ GRANDE', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAJINICUILAPA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUTZAMALA DE PINZ�N', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAMUXTITLAN', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUITZUCO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IGUALA DE LA INDEPENDENCIA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA UNI�N', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARQUELIA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOTITO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OLINAL�', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PETATL�N', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JER�NIMO DE JU�REZ', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS ACATL�N', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS DE LA LOMA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS SAN PEDRO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MARCOS', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAXCO DE ALARC�N', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('T�CPAN DE GALEANA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TELOLOAPAN', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPECOACUILCO DE TRUJANO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIERRA COLORADA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIXTLA DE GUERRERO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLALIXTAQUILLA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAPA DE COMONFORT', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAPEHUALA', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZIHUATANEJO', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZUMPANGO DEL R�O', 12);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACTOPAN', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APAN', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DE FRAY BERNARDINO DE SAHAG�N', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CRUZ AZUL', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUEJUTLA DE REYES', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXMIQUILPAN', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PACHUCA DE SOTO', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO TULANTEPEC', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEAPULCO', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEJI DEL RIO', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIZAYUCA', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAXCOAPAN', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TULA DE ALLENDE', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TULANCINGO', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZIMAPAN', 13);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACATL�N DE JU�REZ', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AHUALULCO DE MERCADO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AJIJIC', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AMECA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARANDAS', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATOTONILCO EL ALTO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AUTL�N DE NAVARRO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHAPALA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIHUATL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD GUZM�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COCULA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COLOTL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL GRULLO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL QUINCE SAN JOS� EL QUINCE', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ETZATL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUADALAJARA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUEJUQUILLA EL ALTO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALOSTOTITL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JAMAY', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOCOTEPEC', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA BARCA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA RESOLANA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAGOS DE MORENO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS PINTITAS', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAGDALENA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOTL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PONCITL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO VALLARTA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN DIEGO DE ALEJANDR�A', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN IGNACIO CERRO GORDO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JOS� EL VERDE EL VERDE', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN DE LOS LAGOS', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JULI�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MIGUEL EL ALTO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAYULA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TALA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TALPA DE ALLENDE', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMAZULA DE GORDIANO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECALITL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEOCALTICHE', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPATITL�N DE MORELOS', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEQUILA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAJOMULCO DE Z��IGA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAQUEPAQUE', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TONAL�', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TOTOTL�N', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXPAN', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('UNI�N DE SAN ANTONIO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE DE GUADALUPE', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA CORONA', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA HIDALGO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YAHUALICA DE GONZ�LEZ GALLO', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACOALCO DE TORRES', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZAPOPAN', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZAPOTILTIC', 14);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALMOLOYA DE JU�REZ', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AMATEPEC', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CAPULHUAC', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHALCO DE D�AZ COVARRUBIAS', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHICONCUAC', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIMALHUAC�N', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD ADOLFO L�PEZ MATEOS', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD NEZAHUALCOYOTL', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COACALCO DE BERRIOZABAL', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTITL�N', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTITL�N IZCALLI', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ECATEPEC DE MORELOS', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUIXQUILUCAN DE DEGOLLADO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXTAPALUCA', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUCHITEPEC DE MARIANO RIVA PALACIO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOS REYES ACAQUILPAN LA PAZ', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MELCHOR OCAMPO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('METEPEC', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAUCALPAN DE JU�REZ', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOYOACAC', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MATEO ATENCO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN VICENTE CHICOLOAPAN DE JU�REZ', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MARIA TULTEPEC', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECAMAC DE FELIPE VILLANUEVA', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEJUPILCO DE HIDALGO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPOTZOTL�N', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEQUIXQUIAC', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEXCOCO DE MORA', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLALNEPANTLA DE BAZ', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TOLUCA DE LERDO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TULTITL�N DE MARIANO ESCOBEDO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE DE CHALCO SOLIDARIDAD', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA NICOL�S ROMERO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XONACATL�N', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZUMPANGO', 15);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APATZING�N DE LA CONSTITUCI�N', 16);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD HIDALGO', 16);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD L�ZARO C�RDENAS', 16);
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COTIJA DE LA PAZ',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUITZEO DEL PORVENIR',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA ZIT�CUARO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUETAMO DE N��EZ',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JACONA DE PLANCARTE',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JIQUILPAN DE JU�REZ',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA PIEDAD DE CABADAS',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS GUACAMAYAS',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOS REYES DE SALGADO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARAVAT�O DE OCAMPO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MORELIA',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVA ITALIA DE RUIZ',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARACHO DE VERDUZCO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('P�TZCUARO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PURU�NDIRO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAHUAYO DE MORELOS',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAC�MBARO DE CODALLOS',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TANGANC�CUARO DE ARISTA',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXPAN',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('URUAPAN',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YUR�CUARO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACAPU',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZAMORA DE HIDALGO',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZINAP�CUARO DE FIGUEROA',16); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTLA CUAUTLA DE MORELOS',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUERNAVACA',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GALEANA',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOJUTLA',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUENTE DE IXTLA',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA ROSA TREINTA',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAQUILTENANGO',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACATEPEC DE HIDALGO',17); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACAPONETA',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AHUACATL�N',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BUCER�AS',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMPOSTELA',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRANCISCO I. MADERO (PUGA)',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXTL�N DEL R�O',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALA',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA PE�ITA DE JALTEMBA',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS VARAS',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RUIZ',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BLAS',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO LAGUNILLAS',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO IXCUINTLA',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECUALA',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPIC',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXPAN',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA HIDALGO EL NUEVO',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XALISCO',18); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AN�HUAC',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CADEREYTA JIM�NEZ',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CI�NEGA DE FLORES',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD APODACA',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD BENITO JU�REZ',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD GENERAL ESCOBEDO',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD SABINAS HIDALGO',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD SANTA CATARINA',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('DOCTOR ARROYO',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL CERCADO',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GARC�A',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUADALUPE',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUALAHUISES',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LINARES',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MONTEMORELOS',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MONTERREY',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN NICOL�S DE LOS GARZA',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO GARZA GARC�A',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO',19); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ASUNCI�N NOCHIXTL�N',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BAHIAS DE HUATULCO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHAHUITES',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD IXTEPEC',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSOLAPA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUIL�PAM DE GUERRERO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL CAMAR�N',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL ROSARIO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE EJUTLA DE CRESPO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE HUAJUAPAN DE LE�N',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE TLAXIACO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUCHIT�N (JUCHIT�N DE ZARAGOZA)',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAGUNAS',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOMA BONITA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARISCALA DE JU�REZ',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAT�AS ROMERO AVENDA�O',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MIAHUATL�N DE PORFIRIO D�AZ',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NATIVIDAD',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OAXACA DE JU�REZ',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOTL�N DE MORELOS',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO ESCONDIDO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUTLA VILLA DE GUERRERO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('R�O GRANDE O PIEDRA PARADA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALINA CRUZ',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN ANTONIO DE LA CAL',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BLAS ATEMPA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FELIPE JALAPA DE D�AZ',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO IXHUAT�N',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO TELIXTLAHUACA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA CUICATL�N',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA LO DE SOTO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA TUXTEPEC',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA VALLE NACIONAL',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN CACAHUATEPEC',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MIGUEL EL GRANDE',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PABLO HUITZO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PABLO VILLA DE MITLA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO MIXTEPEC DTO. 22',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO POCHUTLA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO TAPANATEPEC',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO TOTOLAPA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN SEBASTI�N TECOMAXTLAHUACA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA CRUZ ITUNDUJIA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA LUCIA DEL CAMINO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MAR�A HUATULCO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO JAMILTEPEC',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO JUXTLAHUACA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO PINOTEPA NACIONAL',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO SUCHILQUITONGO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTO DOMINGO TEHUANTEPEC',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEOTITL�N DE FLORES MAG�N',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLACOLULA DE MATAMOROS',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('UNI�N HIDALGO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VICENTE CAMALOTE',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE TAMAZUL�PAM DEL PROGRESO',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE ZAACHILA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA SOLA DE VEGA',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZIMATL�N DE �LVAREZ',20); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACATL�N DE OSORIO',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AMOZOC',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATLIXCO',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD SERD�N',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTLANCINGO',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAUCHINANGO',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IZ�CAR DE MATAMOROS',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUEBLA HEROICA PUEBLA',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN ANDR�S CHOLULA',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MART�N TEXMELUCAN DE LABASTIDA',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO CHOLULA',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECAMACHALCO',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEHUACAN',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEACA',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEZIUTLAN',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XICOTEPEC',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACATL�N',21); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL PUEBLITO',22); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('QUER�TARO',22); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN DEL RIO',22); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BACALAR',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CANC�N',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHETUMAL',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COZUMEL',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FELIPE CARRILLO PUERTO',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ISLA MUJERES',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('KANTUNILK�N',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAYA DEL CARMEN',23); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('C�RDENAS',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CEDRAL',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CERRITOS',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHARCAS',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DEL MA�Z',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD VALLES',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('�BANO',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL NARANJO',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRACCI�N EL REFUGIO',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MATEHUALA',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RIOVERDE',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALINAS DE HIDALGO',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS POTOS�',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MAR�A DEL R�O',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOLEDAD DE GRACIANO S�NCHEZ',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMASOPO',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMAZUNCHALE',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMUIN',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIERRA NUEVA',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE REYES',24); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUARUTO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AHOME',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ANGOSTURA',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHOIX',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSAL�',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CULIAC�N ROSALES',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL ROSARIO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESCUINAPA DE HIDALGO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESTACI�N NARANJO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUAM�CHIL',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUASAVE',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HIGUERA DE ZARAGOZA',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA CRUZ',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LIC. BENITO JU�REZ CAMPO GOBIERNO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOS MOCHIS',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAZATL�N',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOCORITO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAVOLATO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('QUILA',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BLAS',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN IGNACIO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SINALOA DE LEYVA',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TOPOLOBAMPO',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA UNI�N',25); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUA PRIETA',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD OBREG�N',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EMPALME',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HERMOSILLO',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CABORCA',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE CANANEA',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA GUAYMAS',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA NOGALES',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUATABAMPO',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAGDALENA DE KINO',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAVOJOA',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO PE�ASCO',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS R�O COLORADO',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SONOITA',26); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('C�RDENAS',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMALCALCO',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUNDUAC�N',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EMILIANO ZAPATA',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRONTERA',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUIMANGUILLO',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALPA DE M�NDEZ',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MACUSPANA',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARA�SO',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEAPA',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TENOSIQUE DE PINO SU�REZ',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLAHERMOSA',27); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALTAMIRA',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD CAMARGO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD GUSTAVO D�AZ ORDAZ',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MADERO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MANTE',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MIGUEL ALEM�N',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD R�O BRAVO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD TULA',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD VICTORIA',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESTACI�N MANUEL �RSULO GALV�N',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GONZ�LEZ',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA MATAMOROS',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JAUMAVE',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVA CIUDAD GUERRERO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVO LAREDO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('REYNOSA',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FERNANDO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOTO LA MARINA',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMPICO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE HERMOSO',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XICOT�NCATL',28); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APIZACO',29); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CALPULALPAN',29); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIAUTEMPAN',29); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAMANTLA',29); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAXCALA TLAXCALA DE XICOTENCATL',29); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA VICENTE GUERRERO',29); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACAYUCAN',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUA DULCE',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALTOTONGA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALVARADO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('�NGEL R. CABADA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATOYAC',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BANDERILLA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BOCA DEL R�O',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CARLOS A. CARRILLO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CATEMACO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CAZONES DE HERRERA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CERRO AZUL',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COATEPEC',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COATZACOALCOS',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COATZINTLA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('C�RDOBA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSAMALOAPAN',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSOLEACAQUE',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUICHAPA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUITL�HUAC',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL HIGO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FORT�N DE LAS FLORES',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GENERAL MIGUEL ALEM�N POTRERO NUEVO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUTI�RREZ ZAMORA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUATUSCO DE CHICUELLAR',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAYACOCOTLA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUILOAPAN DE CUAUHT�MOC',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ISLA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXTACZOQUITL�N',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('J�LTIPAN DE MORELOS',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOS� CARDEL',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN D�AZ COVARRUBIAS',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN RODR�GUEZ CLARA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS CHOAPAS',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LERDO DE TEJADA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MINATITL�N',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NARANJOS',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NOGALES',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ORIZABA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('P�NUCO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PAPANTLA DE OLARTE',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARAJE NUEVO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PASO DE OVEJAS',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PASO DEL MACHO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAT�N S�NCHEZ',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAYA VICENTE',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('POZA RICA DE HIDALGO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('R�O BLANCO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN ANDR�S TUXTLA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN RAFAEL',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO TUXTLA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SIHUAPAN',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOLEDAD DE DOBLADO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMPICO ALTO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TANTOYUCA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEMPOAL DE S�NCHEZ',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEZONAPA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIERRA BLANCA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIHUATL�N',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLACOJALPAN',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAPACOYAN',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TRES VALLES',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('T�XPAM DE RODR�GUEZ CANO',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VERACRUZ',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XALAPA-ENR�QUEZ',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YECUATLA',30); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('M�RIDA',31); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOTUL DE CARRILLO PUERTO',31); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TICUL',31); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIZIM�N',31); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLADOLID',31); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD CUAUHT�MOC',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRESNILLO',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUADALUPE',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALPA',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JEREZ DE GARC�A SALINAS',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN ALDAMA',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LORETO',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LUIS MOYA',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOYAHUA DE ESTRADA',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NOCHISTL�N DE MEJ�A',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OJOCALIENTE',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('R�O GRANDE',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOMBRERETE',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALPARA�SO',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('V�CTOR ROSALES',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE COS',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA HIDALGO',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLANUEVA',32); 
--INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACATECAS',32);


INSERT INTO build_level002.[date](date_uuid__uniqueidentifier, date_value__date) VALUES(@dateUUID, '2000-01-01');

INSERT INTO build_level002.[session](sess_uuid__uniqueidentifier, sess_starttime__time, sess_lastactivity__time, sess_ipbatch01__tinyint, sess_ipbatch02__tinyint, sess_ipbatch03__tinyint, sess_ipbatch04__tinyint, sess_extradata__varchar, date_uuid__uniqueidentifier, user_uuid_created__uniqueidentifier) VALUES(@sessionStraadUUID, '13:00:00', '13:00:00', NULL, NULL, NULL, NULL, NULL, @dateUUID, @user001UUID);

--INSERT INTO build_level001.[user](user_uuid__uniqueidentifier, user_username__varchar, user_email__varchar, user_cellphone__varchar, user_password__varbinary, user_firstname__varbinary, user_lastname__varbinary, user_roleaccess__tinyint, user_extradata__varchar, reso_uuid_picture__uniqueidentifier, date_uuid_birthdate__uniqueidentifier, city_uuid__uniqueidentifier, sess_uuid_used__uniqueidentifier, sess_uuid_created__uniqueidentifier, user_uuid_root__uniqueidentifier, sess_uuid_deleted__uniqueidentifier) VALUES(
--	@user001UUID, 'admin', NULL, NULL, 
--	HASHBYTES('sha2_512', HASHBYTES('sha2_256', 'admin')), 
--	ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3','Administrator'), 
--	ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', 'Administrator'), 
--	1, NULL, NULL, NULL, NULL, NULL, @sessionStraadUUID, NULL, NULL);

EXEC build_level001.proc_user_insert 
	@user_uuid__uniqueidentifier = @user001UUID, 
	@user_username__varchar = 'admin', 
	@user_email__varchar = NULL, 
	@user_cellphone__varchar = NULL, 
	@user_password__varchar = 'admin', 
	@user_firstname__varchar = 'Administrator', 
	@user_lastname__varchar = 'Administrator', 
	@user_roleaccess__tinyint = 1, 
	@user_extradata__varchar = NULL, 
	@reso_uuid_picture__uniqueidentifier = NULL, 
	@date_uuid_birthdate__uniqueidentifier = NULL, 
	@city_uuid__uniqueidentifier = NULL, 
	@sess_uuid_used__uniqueidentifier = NULL, 
	@sess_uuid_created__uniqueidentifier = @sessionStraadUUID, 
	@user_uuid_root__uniqueidentifier = NULL, 
	@sess_uuid_deleted__uniqueidentifier = NULL;

--EXEC build_level001.proc_user_insert(@user001UUID, 'admin', NULL, NULL, 'admin', 'Administrator', 'Administrator', 1, NULL, NULL, NULL, NULL, NULL, @sessionStraadUUID, NULL, NULL);

GO

ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser373134 FOREIGN KEY (sess_uuid_created__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
ALTER TABLE build_level001.preference ADD CONSTRAINT FKpreference267585 FOREIGN KEY (pref_uuid_root__uniqueidentifier) REFERENCES build_level001.preference (pref_uuid__uniqueidentifier);
ALTER TABLE build_level001.[state] ADD CONSTRAINT FKstate183298 FOREIGN KEY (coun_uuid__uniqueidentifier) REFERENCES build_level001.country (coun_uuid__uniqueidentifier);
ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser138147 FOREIGN KEY (user_uuid_root__uniqueidentifier) REFERENCES build_level001.[user] (user_uuid__uniqueidentifier);
ALTER TABLE build_level001.city ADD CONSTRAINT FKcity970084 FOREIGN KEY (stat_uuid__uniqueidentifier) REFERENCES build_level001.[state] (stat_uuid__uniqueidentifier);
ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser85024 FOREIGN KEY (city_uuid__uniqueidentifier) REFERENCES build_level001.city (city_uuid__uniqueidentifier);
ALTER TABLE build_level002.[session] ADD CONSTRAINT FKsession868286 FOREIGN KEY (user_uuid_created__uniqueidentifier) REFERENCES build_level001.[user] (user_uuid__uniqueidentifier);
ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser377146 FOREIGN KEY (sess_uuid_deleted__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser515892 FOREIGN KEY (sess_uuid_used__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
ALTER TABLE build_level002.[session] ADD CONSTRAINT FKsession541289 FOREIGN KEY (date_uuid__uniqueidentifier) REFERENCES build_level002.[date] (date_uuid__uniqueidentifier);
ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser951719 FOREIGN KEY (reso_uuid_picture__uniqueidentifier) REFERENCES build_level002.[resource] (reso_uuid__uniqueidentifier);
ALTER TABLE build_level001.preference ADD CONSTRAINT FKpreference627068 FOREIGN KEY (user_uuid__uniqueidentifier) REFERENCES build_level001.[user] (user_uuid__uniqueidentifier);
ALTER TABLE build_level001.preference ADD CONSTRAINT FKpreference59225 FOREIGN KEY (sess_uuid_created__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser954464 FOREIGN KEY (date_uuid_birthdate__uniqueidentifier) REFERENCES build_level002.[date] (date_uuid__uniqueidentifier);
ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc68501 FOREIGN KEY (city_uuid__uniqueidentifier) REFERENCES build_level001.city (city_uuid__uniqueidentifier);
ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc633898 FOREIGN KEY (prco_uuid_root__uniqueidentifier) REFERENCES build_level003.principalcompany (prco_uuid__uniqueidentifier);
ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc389657 FOREIGN KEY (sess_uuid_created__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
ALTER TABLE build_level003.[version] ADD CONSTRAINT FKversion840117 FOREIGN KEY (date_uuid__uniqueidentifier) REFERENCES build_level002.[date] (date_uuid__uniqueidentifier);
ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc114089 FOREIGN KEY (reso_uuid_logo__uniqueidentifier) REFERENCES build_level002.[resource] (reso_uuid__uniqueidentifier);

GO

CREATE INDEX state_stat_name__varchar ON build_level001.[state] (stat_name__varchar);
CREATE UNIQUE INDEX country_coun_code__varchar ON build_level001.country (coun_code__varchar);
CREATE INDEX user_user_username__varchar ON build_level001.[user] (user_username__varchar);
CREATE INDEX user_user_email__varchar ON build_level001.[user] (user_email__varchar);
CREATE INDEX user_user_cellphone__varchar ON build_level001.[user] (user_cellphone__varchar);
CREATE INDEX city_city_name__varchar ON build_level001.city (city_name__varchar);
CREATE UNIQUE INDEX date_date_value__date ON build_level002.[date] (date_value__date);
CREATE INDEX resource_reso_name__varchar ON build_level002.[resource] (reso_name__varchar);



---------------------------------------------


--CREATE DATABASE _dbname_;

--GO

--USE _dbname_;

--GO

--CREATE LOGIN _internal_ WITH PASSWORD = N'_pspsps_', DEFAULT_DATABASE = [_dbname_], DEFAULT_LANGUAGE = [Spanish], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF; CREATE USER _internal_ FOR LOGIN [_internal_]; ALTER ROLE [db_datareader] ADD MEMBER [_internal_]; ALTER ROLE [db_datawriter] ADD MEMBER [_internal_];

--GO
--CREATE SCHEMA build_level000;
--GO
--CREATE SCHEMA build_level001;
--GO
--CREATE SCHEMA build_level002;
--GO
--CREATE SCHEMA build_level003;
--GO
--CREATE SCHEMA build_level004;

--GO

--CREATE TABLE build_level001.[state] (stat_uuid__uniqueidentifier uniqueidentifier NOT NULL, stat_name__varchar varchar(100) NOT NULL, coun_uuid__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (stat_uuid__uniqueidentifier));
--CREATE TABLE build_level001.country (coun_uuid__uniqueidentifier uniqueidentifier NOT NULL, coun_code__varchar varchar(6) NOT NULL, coun_name__varchar varchar(100) NOT NULL, PRIMARY KEY (coun_uuid__uniqueidentifier));
--CREATE TABLE build_level001.[user] (user_uuid__uniqueidentifier uniqueidentifier NOT NULL, user_username__varchar varchar(100) NULL, user_email__varchar varchar(100) NULL, user_cellphone__varchar varchar(10) NULL, user_password__varbinary varbinary(max) NOT NULL, user_firstname__varchar varchar(max) NOT NULL, user_lastname__varchar varchar(max) NOT NULL, user_roleaccess__tinyint tinyint NOT NULL, user_extradata__varchar varchar(max) NULL, reso_uuid_picture__uniqueidentifier uniqueidentifier NULL, date_uuid_birthdate__uniqueidentifier uniqueidentifier NULL, city_uuid__uniqueidentifier uniqueidentifier NULL, sess_uuid_used__uniqueidentifier uniqueidentifier NULL, sess_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, user_uuid_root__uniqueidentifier uniqueidentifier NULL, sess_uuid_deleted__uniqueidentifier uniqueidentifier NULL, PRIMARY KEY (user_uuid__uniqueidentifier));
--CREATE TABLE build_level001.city (city_uuid__uniqueidentifier uniqueidentifier NOT NULL, city_name__varchar varchar(100) NOT NULL, stat_uuid__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (city_uuid__uniqueidentifier));
--CREATE TABLE build_level002.[session] (sess_uuid__uniqueidentifier uniqueidentifier NOT NULL, sess_starttime__time time(0) NOT NULL, sess_lastactivity__time time(0) NOT NULL, sess_ipbatch01__tinyint tinyint NULL, sess_ipbatch02__tinyint tinyint NULL, sess_ipbatch03__tinyint tinyint NULL, sess_ipbatch04__tinyint tinyint NULL, sess_extradata__varchar varchar(max) NULL, date_uuid__uniqueidentifier uniqueidentifier NOT NULL, user_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (sess_uuid__uniqueidentifier));
--CREATE TABLE build_level002.[date] (date_uuid__uniqueidentifier uniqueidentifier NOT NULL, date_value__date date NOT NULL, PRIMARY KEY (date_uuid__uniqueidentifier));
--CREATE TABLE build_level002.[resource] (reso_uuid__uniqueidentifier uniqueidentifier NOT NULL, reso_name__varchar varchar(100) NOT NULL, reso_extension__tinyint tinyint NOT NULL, reso_description__varchar varchar(max) NULL, reso_value__varbinary varbinary(max) NULL, reso_externalurlorname__varchar varchar(max) NULL, PRIMARY KEY (reso_uuid__uniqueidentifier));
--CREATE TABLE build_level001.preference (pref_uuid__uniqueidentifier uniqueidentifier NOT NULL, pref_type__int int NOT NULL, pref_value__bit bit NULL, pref_value__tinyint tinyint NULL, pref_value__int int NULL, pref_value__varchar varchar(max) NULL, user_uuid__uniqueidentifier uniqueidentifier NOT NULL, sess_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, pref_uuid_root__uniqueidentifier uniqueidentifier NULL, PRIMARY KEY (pref_uuid__uniqueidentifier));
--CREATE TABLE build_level003.principalcompany (prco_uuid__uniqueidentifier uniqueidentifier NOT NULL, prco_rfc__varchar varchar(13) NULL, prco_name__varchar varchar(100) NOT NULL, prco_address__varchar varchar(255) NULL, prco_phone__varchar varchar(100) NULL, prco_email__varchar varchar(100) NULL, prco_facebook__varchar varchar(100) NULL, prco_developmentmode__bit bit NOT NULL, prco_timebetweenbackups__timestamp timestamp NULL, reso_uuid_logo__uniqueidentifier uniqueidentifier NULL, city_uuid__uniqueidentifier uniqueidentifier NULL, sess_uuid_created__uniqueidentifier uniqueidentifier NOT NULL, prco_uuid_root__uniqueidentifier uniqueidentifier NULL, PRIMARY KEY (prco_uuid__uniqueidentifier));
--CREATE TABLE build_level003.[version] (vers_uuid__uniqueidentifier uniqueidentifier NOT NULL, vers_name__varchar varchar(12) NOT NULL, date_uuid__uniqueidentifier uniqueidentifier NOT NULL, PRIMARY KEY (vers_uuid__uniqueidentifier));

--GO

----CREATE PROCEDURE build_level000.proc_systemserver_encrypt 
----	@value VARCHAR(MAX)
----	AS
----		--SELECT HASHBYTES('sha2_512', HASHBYTES('sha2_256', @value))
----		SELECT CONVERT(VARCHAR(MAX), HASHBYTES('sha2_512', HASHBYTES('sha2_256', 'admin')))

----GO

----EXEC build_level000.proc_systemserver_encrypt 'Hola'
----EXEC build_level000.proc_systemserver_encrypt 'Hola1'
----SELECT user_password__varbinary FROM build_level001.[user]

----GO

----CREATE FUNCTION build_level000.func_systemserver_getdatetime01()
----	RETURNS DATETIME
----	AS
----	BEGIN 
----		DECLARE @datetime DATETIME
----		SELECT @datetime = GETDATE()
----		RETURN @datetime
----	END;

----GO

----CREATE FUNCTION build_level000.func_systemserver_getdatetime02()
----	RETURNS INT
----	AS
----	BEGIN 
----		RETURN 1
----	END;

----select build_level000.func_systemserver_getdatetime();
----SELECT GETDATE()

----GO

--CREATE PROCEDURE build_level000.proc_systemserver_getdatetime
--	@dateTime VARCHAR OUTPUT
--	AS
--		SELECT CONVERT(VARCHAR(24), GETDATE(), 120) 
----	RETURN 1

--GO

--CREATE FUNCTION build_level000.func_systemserver_generatehashpassword(
--	@value VARCHAR(MAX))
--	RETURNS VARBINARY(MAX)
--	AS
--	BEGIN 
--		DECLARE @result VARBINARY(MAX)

--		SELECT @result = HASHBYTES('sha2_512', HASHBYTES('sha2_256', @value))

--		RETURN @result
--	END;

--GO

--CREATE PROCEDURE build_level000.proc_systemserver_comparepassword
--	@user_uuid__uniqueidentifier UNIQUEIDENTIFIER,
--	@password VARCHAR(MAX)
--	AS
--	DECLARE @generatePassword VARBINARY(MAX)

--	DECLARE @ok BIT	
--	SET @ok = 0

--	SELECT @generatePassword = build_level000.func_systemserver_generatehashpassword(@password)

--	SELECT @ok = 1 FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier AND user_password__varbinary = @generatePassword)

--	SELECT @ok

--GO

--CREATE FUNCTION build_level000.func_systemserver_encryptdecrypt(
--	@value VARCHAR(MAX),
--	@encrypt BIT)
--	RETURNS VARCHAR(MAX)
--	AS
--	BEGIN 
--		DECLARE @password VARCHAR(MAX)
--		DECLARE @result VARCHAR(MAX)

--		SET @password = 'd-f5]8T.x6_[s3'

--		IF(@encrypt = 1)		
--			SELECT @result = ENCRYPTBYPASSPHRASE(@password, @value);
--		ELSE
--			SELECT @result = CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE(@password, @value));

--		RETURN @result
--	END;

--GO

----CREATE PROCEDURE build_level000.proc_systemserver_getdatetime
----	@dateTime DATETIME OUTPUT
----	AS
----		SELECT @dateTime = GETDATE()
----	RETURN 1

----SELECT CONVERT(VARCHAR(24),GETDATE(),113) 
----DECLARE @dateTime VARCHAR
----EXEC build_level000.proc_systemserver_getdatetime @dateTime OUTPUT;
----SELECT @dateTime

----GO

--CREATE PROCEDURE build_level001.proc_user_insert 
--	@user_uuid__uniqueidentifier uniqueidentifier, 
--	@user_username__varchar varchar(100), 
--	@user_email__varchar varchar(100), 
--	@user_cellphone__varchar varchar(10), 
--	@user_password__varchar varchar(max), 
--	@user_firstname__varchar varchar(max), 
--	@user_lastname__varchar varchar(max), 
--	@user_roleaccess__tinyint tinyint, 
--	@user_extradata__varchar varchar(max), 
--	@reso_uuid_picture__uniqueidentifier uniqueidentifier, 
--	@date_uuid_birthdate__uniqueidentifier uniqueidentifier, 
--	@city_uuid__uniqueidentifier uniqueidentifier, 
--	@sess_uuid_used__uniqueidentifier uniqueidentifier, 
--	@sess_uuid_created__uniqueidentifier uniqueidentifier, 
--	@user_uuid_root__uniqueidentifier uniqueidentifier, 
--	@sess_uuid_deleted__uniqueidentifier uniqueidentifier
--	AS
--		BEGIN TRY
--			--IF((SELECT COUNT(*) FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier)) > 0)
--			--BEGIN
--			--	RETURN 0
--			--END

--			BEGIN TRAN tranOperation
--				INSERT INTO build_level001.[user](
--					user_uuid__uniqueidentifier, 
--					user_username__varchar, 
--					user_email__varchar, 
--					user_cellphone__varchar, 
--					user_password__varbinary, 
--					user_firstname__varchar, 
--					user_lastname__varchar, 
--					user_roleaccess__tinyint, 
--					user_extradata__varchar, 
--					reso_uuid_picture__uniqueidentifier, 
--					date_uuid_birthdate__uniqueidentifier, 
--					city_uuid__uniqueidentifier, 
--					sess_uuid_used__uniqueidentifier, 
--					sess_uuid_created__uniqueidentifier, 
--					user_uuid_root__uniqueidentifier, 
--					sess_uuid_deleted__uniqueidentifier) 
--				VALUES(
--					@user_uuid__uniqueidentifier, 
--					@user_username__varchar, 
--					@user_email__varchar, 
--					@user_cellphone__varchar, 
--					build_level000.func_systemserver_generatehashpassword(@user_password__varchar),
--					build_level000.func_systemserver_encryptdecrypt(@user_firstname__varchar, 1),
--					build_level000.func_systemserver_encryptdecrypt(@user_lastname__varchar, 1),
--					--HASHBYTES('sha2_512', HASHBYTES('sha2_256', @user_password__varbinary)),
--					--ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', @user_firstname__varchar), 
--					--ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', @user_lastname__varchar), 
--					@user_roleaccess__tinyint, 
--					@user_extradata__varchar, 
--					@reso_uuid_picture__uniqueidentifier, 
--					@date_uuid_birthdate__uniqueidentifier, 
--					@city_uuid__uniqueidentifier, 
--					@sess_uuid_used__uniqueidentifier, 
--					@sess_uuid_created__uniqueidentifier, 
--					@user_uuid_root__uniqueidentifier, 
--					@sess_uuid_deleted__uniqueidentifier
--				);
--			COMMIT TRAN tranOperation

--			RETURN 1

--		END TRY

--		BEGIN CATCH
--			ROLLBACK TRAN tranOperation
--		END CATCH

--GO

----CREATE PROCEDURE build_level000.proc_systemserver_encrypt
----	@password VARCHAR(MAX)
----	AS
----	SELECT build_level000.func_systemserver_generatehashpassword(@password)

----EXEC build_level000.proc_systemserver_comparepassword 'A1637AF8-4BD3-4821-A1E3-94C24469C626', 'admN'
----SELECT * FROM build_level001.[user];

----GO

----CREATE FUNCTION build_level000.func_systemserver_encrypt(
----	@value VARCHAR(MAX)
----	)
----	RETURNS VARCHAR(MAX)
----	AS
----	BEGIN 
----		DECLARE @password VARCHAR(MAX)
----		DECLARE @result VARCHAR(MAX)

----		SELECT @result = ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', @value);

----		RETURN @result
----	END;

----GO

----SELECT HASHBYTES('sha2_512', HASHBYTES('sha2_256', 'admin'))
----SELECT build_level000.func_systemserver_generatehashpassword('admin')
----DECLARE @varbinaryField varbinary(max);
----SET @varbinaryField = 0x21232F297A57A5A743894A0E4A801FC3;

----SELECT CONVERT(varchar(max),@varbinaryField,2), 
----@varbinaryField

----SELECT CONVERT(VARCHAR(MAX), user_password__varbinary) FROM build_level001.[user]
----SELECT build_level000.func_systemserver_encryptdecrypt((SELECT u.user_firstname__varchar FROM build_level001.[user] AS u WHERE(u.user_uuid__uniqueidentifier = 'A1637AF8-4BD3-4821-A1E3-94C24469C626')), 0);
----EXEC build_level001.proc_user_select 'A1637AF8-4BD3-4821-A1E3-94C24469C626'
----SELECT * FROM build_level001.[user]

--GO

--CREATE PROCEDURE build_level001.proc_user_select 
--	@user_uuid__uniqueidentifier UNIQUEIDENTIFIER
--	AS
--		SELECT
--			user_uuid__uniqueidentifier, 
--			user_username__varchar, 
--			user_email__varchar, 
--			user_cellphone__varchar, 
--			user_password__varbinary, 
--			CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3',user_firstname__varchar)) AS user_firstname__varchar, --It works
--			CAST(DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', user_lastname__varchar) AS VARCHAR(MAX)) AS user_lastname__varchar, --It works			
--			--build_level000.func_systemserver_encryptdecrypt(user_firstname__varchar, 0) AS user_firstname__varchar,--X
--			--build_level000.func_systemserver_encryptdecrypt(user_lastname__varchar, 0) AS user_lastname__varchar,--X
--			--CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3',user_firstname__varchar)) AS user_firstname__varchar,--X
--			--CAST(DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', user_lastname__varchar) AS VARCHAR(MAX)) AS user_lastname__varchar, --X
--			user_roleaccess__tinyint, 
--			user_extradata__varchar, 
--			reso_uuid_picture__uniqueidentifier, 
--			date_uuid_birthdate__uniqueidentifier, 
--			city_uuid__uniqueidentifier, 
--			sess_uuid_used__uniqueidentifier, 
--			sess_uuid_created__uniqueidentifier, 
--			user_uuid_root__uniqueidentifier, 
--			sess_uuid_deleted__uniqueidentifier
--		FROM [build_level001].[user] WHERE([user_uuid__uniqueidentifier] = @user_uuid__uniqueidentifier);

--GO

----SELECT * FROM build_level001.[user]
----EXEC build_level001.proc_user_select 'F1459AD2-0D89-45E3-8414-ADF6A02BEBA5'
----SELECT ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')
----SELECT CAST(DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', '') AS VARCHAR(MAX)) 
----DECLARE @psw BYTE(MAX)
----SELECT @psw = ENCRYPTBYPASSPHRASE('KurroSoftware', 'MyPassword')
----SELECT @psw
----SELECT CAST(DECRYPTBYPASSPHRASE('KurroSoftware', ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')) AS VARCHAR(MAX))
----SELECT CAST(DECRYPTBYPASSPHRASE('KurroSoftware', '0x010000003F3EE002D407783EC8B013CB1DA517DBC174056729A517B22CBA8DD70A3AD00C') AS VARCHAR(MAX))
----SELECT DECRYPTBYPASSPHRASE('KurroSoftware', '0x010000003F3EE002D407783EC8B013CB1DA517DBC174056729A517B22CBA8DD70A3AD00C')

----SELECT * FROM [build_level001].[user]
----EXEC build_level001.proc_user_select '60B20288-4476-45EA-8089-5EAA29C551BA'

----DROP PROCEDURE build_level000.proc_systemserver_decrypt
----	@value VARCHAR(MAX),
----	@password VARCHAR(MAX)
----	AS
----	SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE(@password, @value)) AS result;


----SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3','USER')) AS user_firstname__varchar
----SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('d-f5]8T.x6_[s3','USER')) AS user_firstname__varchar
----SELECT ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')
----SELECT ENCRYPTBYPASSPHRASE('KurroSoftware','MyPassword')

----EXEC build_level000.proc_systemserver_encryptdecrypt 'Pass', 'Hola', 1
----EXEC build_level000.proc_systemserver_encryptdecrypt 'KurroSoftware', '0x01000000FD9603A6A6BA4BFB1CA27CD42ED8E006FC04ABEFECFB65C0E76001D126DA4040', 0

----CREATE PROCEDURE build_level000.proc_systemserver_encryptdecrypt
----	@password VARCHAR(MAX),
----	@value VARCHAR(MAX),
----	@encrypt BIT
----	AS
----	DECLARE @result VARCHAR(MAX)

----	IF(@encrypt = 1)
----		BEGIN			
----			SELECT @result = ENCRYPTBYPASSPHRASE(@password, @value);
----		END
----	ELSE
----		BEGIN
----			SELECT @result = CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE(@password, @value));
----		END

----	SELECT @result

----GO

--CREATE PROCEDURE build_level001.proc_user_findLastBranch
--	@user_uuid__uniqueidentifier______Current uniqueidentifier,
--	@user_uuid__uniqueidentifier______Last uniqueidentifier OUTPUT
--	AS
--		WITH EmpCTE ([user_uuid__uniqueidentifier], [user_username__varchar], [user_email__varchar], [user_cellphone__varchar], [user_uuid_root__uniqueidentifier])
--		AS
--		(
--			SELECT [user_uuid__uniqueidentifier], [user_username__varchar], [user_email__varchar], [user_cellphone__varchar], [user_uuid_root__uniqueidentifier]
--				FROM build_level001.[user]
--				WHERE [user_uuid__uniqueidentifier] = @user_uuid__uniqueidentifier______Current
--			UNION ALL
--		--Recursiveness
--			SELECT e.[user_uuid__uniqueidentifier], e.[user_username__varchar], e.[user_email__varchar], e.[user_cellphone__varchar], e.[user_uuid_root__uniqueidentifier]
--				FROM build_level001.[user] AS e JOIN EmpCTE AS m
--					ON e.user_uuid_root__uniqueidentifier = m.[user_uuid__uniqueidentifier]
--		)
--		SELECT @user_uuid__uniqueidentifier______Last = user_uuid__uniqueidentifier FROM EmpCTE

--	RETURN 1

--GO

--CREATE TRIGGER trigger_user_update
--ON build_level001.[user]
--INSTEAD OF UPDATE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--		-- Inserta aqu� las instrucciones

--		BEGIN TRY
--			BEGIN TRAN tranOperation
--				--
--				--SELECT * FROM inserted



--				DECLARE @user_uuid__uniqueidentifier___New uniqueidentifier
--				DECLARE @user_username__varchar varchar(100)
--				DECLARE @user_email__varchar varchar(100)
--				DECLARE @user_cellphone__varchar varchar(10)
--				DECLARE @user_password__varbinary varbinary(max)
--				DECLARE @user_firstname__varchar varchar(max)
--				DECLARE @user_lastname__varchar varchar(max)
--				DECLARE @user_roleaccess__tinyint tinyint
--				DECLARE @user_extradata__varchar varchar(max)
--				DECLARE @reso_uuid_picture__uniqueidentifier uniqueidentifier
--				DECLARE @date_uuid_birthdate__uniqueidentifier uniqueidentifier
--				DECLARE @city_uuid__uniqueidentifier uniqueidentifier
--				--DECLARE @sess_uuid_used__uniqueidentifier uniqueidentifier
--				DECLARE @sess_uuid_created__uniqueidentifier uniqueidentifier
--				--DECLARE @user_uuid_root__uniqueidentifier uniqueidentifier
--				DECLARE @sess_uuid_deleted__uniqueidentifier uniqueidentifier



--				-- We get a new UUID for the copy register
--				SET @user_uuid__uniqueidentifier___New = NEWID()
--				WHILE((SELECT COUNT(*) FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier___New)) > 0)
--				BEGIN
--					SET @user_uuid__uniqueidentifier___New = NEWID()
--				END



--				DECLARE @user_uuid__uniqueidentifier______Current uniqueidentifier
--				SELECT @user_uuid__uniqueidentifier______Current = user_uuid__uniqueidentifier FROM inserted



--				SELECT TOP 1
--					--@user_uuid__uniqueidentifier_New = user_uuid__uniqueidentifier, 
--					@user_username__varchar = user_username__varchar, 
--					@user_email__varchar = user_email__varchar, 
--					@user_cellphone__varchar = user_cellphone__varchar, 
--					@user_password__varbinary = user_password__varbinary, 
--					@user_firstname__varchar = user_firstname__varchar, 
--					@user_lastname__varchar = user_lastname__varchar, 
--					@user_roleaccess__tinyint = user_roleaccess__tinyint, 
--					@user_extradata__varchar = user_extradata__varchar, 
--					@reso_uuid_picture__uniqueidentifier = reso_uuid_picture__uniqueidentifier, 
--					@date_uuid_birthdate__uniqueidentifier = date_uuid_birthdate__uniqueidentifier, 
--					@city_uuid__uniqueidentifier = city_uuid__uniqueidentifier, 
--					--sess_uuid_used__uniqueidentifier 
--					@sess_uuid_created__uniqueidentifier = sess_uuid_created__uniqueidentifier, 
--					--user_uuid_root__uniqueidentifier
--					@sess_uuid_deleted__uniqueidentifier = sess_uuid_deleted__uniqueidentifier
--					FROM build_level001.[user] WHERE(user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier______Current);



--				--SELECT @user_uuid__uniqueidentifier



--				-- We get last ID of the list (Recursiveness)
--				DECLARE @user_uuid__uniqueidentifier______Last uniqueidentifier
--				EXEC build_level001.proc_user_findLastBranch @user_uuid__uniqueidentifier______Current = @user_uuid__uniqueidentifier______Current,  @user_uuid__uniqueidentifier______Last = @user_uuid__uniqueidentifier______Last OUTPUT;
				


--				-- First we store the copy register
--				INSERT INTO build_level001.[user](user_uuid__uniqueidentifier, user_username__varchar, user_email__varchar, user_cellphone__varchar, user_password__varbinary, user_firstname__varchar, user_lastname__varchar, user_roleaccess__tinyint, user_extradata__varchar, reso_uuid_picture__uniqueidentifier, date_uuid_birthdate__uniqueidentifier, city_uuid__uniqueidentifier, sess_uuid_used__uniqueidentifier, sess_uuid_created__uniqueidentifier, user_uuid_root__uniqueidentifier, sess_uuid_deleted__uniqueidentifier) 
--					VALUES(
--						@user_uuid__uniqueidentifier___New,
--						@user_username__varchar,
--						@user_email__varchar,
--						@user_cellphone__varchar, 
--						@user_password__varbinary, 
--						@user_firstname__varchar, 
--						@user_lastname__varchar, 
--						@user_roleaccess__tinyint, 
--						@user_extradata__varchar, 
--						@reso_uuid_picture__uniqueidentifier, 
--						@date_uuid_birthdate__uniqueidentifier, 
--						@city_uuid__uniqueidentifier, 
--						NULL, 
--						@sess_uuid_created__uniqueidentifier, 
--						@user_uuid__uniqueidentifier______Last, 
--						@sess_uuid_deleted__uniqueidentifier
--						);



--				SELECT 
--					--@user_uuid__uniqueidentifier_New = user_uuid__uniqueidentifier, 
--					@user_username__varchar = user_username__varchar, 
--					@user_email__varchar = user_email__varchar, 
--					@user_cellphone__varchar = user_cellphone__varchar, 
--					@user_password__varbinary = user_password__varbinary, 
--					@user_firstname__varchar = user_firstname__varchar, 
--					@user_lastname__varchar = user_lastname__varchar, 
--					@user_roleaccess__tinyint = user_roleaccess__tinyint, 
--					@user_extradata__varchar = user_extradata__varchar, 
--					@reso_uuid_picture__uniqueidentifier = reso_uuid_picture__uniqueidentifier, 
--					@date_uuid_birthdate__uniqueidentifier = date_uuid_birthdate__uniqueidentifier, 
--					@city_uuid__uniqueidentifier = city_uuid__uniqueidentifier, 
--					--sess_uuid_used__uniqueidentifier, 
--					@sess_uuid_created__uniqueidentifier = sess_uuid_created__uniqueidentifier,
--					--user_uuid_root__uniqueidentifier 
--					@sess_uuid_deleted__uniqueidentifier = sess_uuid_deleted__uniqueidentifier
--					FROM inserted



--				UPDATE build_level001.[user] SET  
--					user_username__varchar = @user_username__varchar, 
--					user_email__varchar = @user_email__varchar, 
--					user_cellphone__varchar = @user_cellphone__varchar, 
--					user_password__varbinary = @user_password__varbinary, 
--					user_firstname__varchar = @user_firstname__varchar, 
--					user_lastname__varchar = @user_lastname__varchar, 
--					user_roleaccess__tinyint = @user_roleaccess__tinyint, 
--					user_extradata__varchar = @user_extradata__varchar, 
--					reso_uuid_picture__uniqueidentifier = @reso_uuid_picture__uniqueidentifier, 
--					date_uuid_birthdate__uniqueidentifier = @date_uuid_birthdate__uniqueidentifier, 
--					city_uuid__uniqueidentifier = @city_uuid__uniqueidentifier, 
--					sess_uuid_used__uniqueidentifier = NULL, 
--					sess_uuid_created__uniqueidentifier = @sess_uuid_created__uniqueidentifier, 
--					user_uuid_root__uniqueidentifier = NULL, 
--					sess_uuid_deleted__uniqueidentifier = @sess_uuid_deleted__uniqueidentifier
--					WHERE([user].user_uuid__uniqueidentifier = @user_uuid__uniqueidentifier______Current)


--				--
--			COMMIT TRAN tranOperation
--		END TRY

--		BEGIN CATCH
--			ROLLBACK TRAN tranOperation
--		END CATCH
--	END

--GO

----UPDATE build_level001.[user] SET user_email__varchar = 'HOLA3' WHERE(user_uuid__uniqueidentifier = '8B319333-003F-4363-ADD6-043C6FAA320D');
----SELECT * FROM build_level001.[user];

----DROP TRIGGER [build_level001].[trigger_user_edit]

----GO

--CREATE TRIGGER trigger_country_delete
--ON build_level001.country
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_state_delete
--ON build_level001.[state]
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_city_delete
--ON build_level001.city
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_user_delete
--ON build_level001.[user]
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_preference_delete
--ON build_level001.preference
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_session_delete
--ON build_level002.[session]
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_date_delete
--ON build_level002.[date]
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_resource_delete
--ON build_level002.[resource]
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_principalcompany_delete
--ON build_level003.principalcompany
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--CREATE TRIGGER trigger_version_delete
--ON build_level003.[version]
--INSTEAD OF DELETE
--AS
--	BEGIN
--		SET NOCOUNT ON;--Para impedir que una instrucci�n de asignaci�n devuelva un resultado
--	END

--GO

--DECLARE @countryMexicoUUID uniqueidentifier
--DECLARE @stateDurangoUUID uniqueidentifier
--DECLARE @stateCoahuilaUUID uniqueidentifier
--DECLARE @cityGomezPalacioUUID uniqueidentifier
--DECLARE @cityTorreonUUID uniqueidentifier
--DECLARE @dateUUID uniqueidentifier
--DECLARE @sessionStraadUUID uniqueidentifier
--DECLARE @user001UUID uniqueidentifier

--SET @countryMexicoUUID = NEWID()
--SET @stateDurangoUUID = NEWID()
--SET @stateCoahuilaUUID = NEWID()
--SET @cityGomezPalacioUUID = NEWID()
--SET @cityTorreonUUID = NEWID()
--SET @dateUUID = NEWID()
--SET @sessionStraadUUID = NEWID()
--SET @user001UUID = NEWID()

--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AF', 'Afganist�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AL', 'Albania');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DE', 'Alemania');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AD', 'Andorra');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AO', 'Angola');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AI', 'Anguila');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AQ', 'Ant�rtida');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AG', 'Antigua y Barbuda');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SA', 'Arabia Saudita');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DZ', 'Argelia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AR', 'Argentina');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AM', 'Armenia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AW', 'Aruba');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AU', 'Australia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AT', 'Austria');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AZ', 'Azerbaiy�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BS', 'Bahamas');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BD', 'Banglad�s');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BB', 'Barbados');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BH', 'Bar�in');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BE', 'B�lgica');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BZ', 'Belice');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BJ', 'Ben�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BM', 'Bermudas');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BY', 'Bielorrusia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BO', 'Bolivia, Estado Plurinacional de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BQ', 'Bonaire, San Eustaquio y Saba');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BA', 'Bosnia y Herzegovina');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BW', 'Botsuana');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BR', 'Brasil');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BN', 'Brun�i Darussalam');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BG', 'Bulgaria');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BF', 'Burkina Faso');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BI', 'Burundi');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BT', 'But�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CV', 'Cabo Verde');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KH', 'Camboya');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CM', 'Camer�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CA', 'Canad�');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TD', 'Chad');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CL', 'Chile');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CN', 'China, Rep�blica Popular');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CY', 'Chipre');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CO', 'Colombia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KM', 'Comoras');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CD', 'Congo, La Rep�blica Democr�tica del');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CG', 'Congo');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KR', 'Corea, Rep�blica de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KP', 'Corea, Rep�blica Democr�tica Popular de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CI', 'Costa de Marfil');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CR', 'Costa Rica');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HR', 'Croacia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CU', 'Cuba');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CW', 'Curazao');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DK', 'Dinamarca');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DM', 'Dominica');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EC', 'Ecuador');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EG', 'Egipto');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SV', 'El Salvador');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AE', 'Emiratos �rabes Unidos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ER', 'Eritrea');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SK', 'Eslovaquia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SI', 'Eslovenia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ES', 'Espa�a');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'US', 'Estados Unidos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EE', 'Estonia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ET', 'Etiop�a');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RU', 'Federacion Rusa');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PH', 'Filipinas');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FI', 'Finlandia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FJ', 'Fiyi');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FR', 'Francia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GA', 'Gab�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GM', 'Gambia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GE', 'Georgia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GH', 'Ghana');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GI', 'Gibraltar');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GD', 'Granada');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GR', 'Grecia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GL', 'Groenlandia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GP', 'Guadalupe');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GU', 'Guam');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GT', 'Guatemala');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GF', 'Guayana Francesa');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GG', 'Guernsey');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GW', 'Guinea-Bis�u');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GQ', 'Guinea Ecuatorial');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GN', 'Guinea');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GY', 'Guyana');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HT', 'Hait�');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HN', 'Honduras');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HK', 'Hong Kong');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HU', 'Hungr�a');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IN', 'India');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ID', 'Indonesia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IQ', 'Irak');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IR', 'Ir�n, Rep�blica Isl�mica de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IE', 'Irlanda');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BV', 'Isla Bouvet');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IM', 'Isla de Man');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CX', 'Isla de Navidad');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NF', 'Isla Norfolk');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IS', 'Islandia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AX', 'Islas �land');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KY', 'Islas Caim�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CC', 'Islas Cocos (Keeling)');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CK', 'Islas Cook');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FK', 'Islas Falkland (Malvinas)');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FO', 'Islas Feroe');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GS', 'Islas Georgias del Sur y Sandwich del Sur');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'HM', 'Islas Heard y Mcdonald');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MP', 'Islas Marianas del Norte');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MH', 'Islas Marshall');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SB', 'Islas Salom�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TC', 'Islas Turcas y Caicos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UM', 'Islas Ultramarinas Menores de Estados Unidos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VG', 'Islas Virgenes Brit�nicas');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VI', 'Islas Virgenes de Los Estados Unidos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IL', 'Israel');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IT', 'Italia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JM', 'Jamaica');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JP', 'Jap�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JE', 'Jersey');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'JO', 'Jordania');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KZ', 'Kazajist�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KE', 'Kenia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KG', 'Kirguist�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KI', 'Kiribati');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KW', 'Kuwait');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LS', 'Lesoto');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LV', 'Letonia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LB', 'L�bano');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LR', 'Liberia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LY', 'Libia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LI', 'Liechtenstein');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LT', 'Lituania');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LU', 'Luxemburgo');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MO', 'Macao');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MK', 'Macedonia, La Antigua Rep�blica Yugoslava de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MG', 'Madagascar');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MY', 'Malasia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MW', 'Malaui');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MV', 'Maldivas');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ML', 'Mal�');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MT', 'Malta');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MA', 'Marruecos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MQ', 'Martinica');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MU', 'Mauricio');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MR', 'Mauritania');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'YT', 'Mayotte');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(@countryMexicoUUID, 'MX', 'M�xico');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'FM', 'Micronesia, Estados Federados de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MD', 'Moldavia, Rep�blica de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MC', 'M�naco');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MN', 'Mongolia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ME', 'Montenegro');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MS', 'Montserrat');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MZ', 'Mozambique');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MM', 'Myanmar');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NA', 'Nabimia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NR', 'Nauru');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NP', 'Nepal');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NI', 'Nicaragua');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NG', 'Nigeria');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NE', 'N�ger');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NU', 'Niue');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NO', 'Noruega');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NC', 'Nueva Caledonia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NZ', 'Nueva Zelanda');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'OM', 'Om�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'NL', 'Pa�ses Bajos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PK', 'Pakist�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PW', 'Palaos');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PS', 'Palestina');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PA', 'Panam�');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PG', 'Pap�a Nueva Guinea');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PY', 'Paraguay');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PE', 'Per�');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PN', 'Pitcairn');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PF', 'Polinesia Francesa');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PL', 'Polonia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PT', 'Portugal');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PR', 'Puerto Rico');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'QA', 'Qatar');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'GB', 'Reino Unido');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CF', 'Rep�blica Centroafricana');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CZ', 'Rep�blica Checa');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LA', 'Rep�blica Democr�tica Popular Lao');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DO', 'Rep�blica Dominicana');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RE', 'Reuni�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RW', 'Ruanda');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RO', 'Rumania');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'EH', 'Sahara Occidental');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'AS', 'Samoa Americana');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'WS', 'Samoa');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'BL', 'San Bartolom�');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'KN', 'San Crist�bal y Nieves');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SM', 'San Marino');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'MF', 'San Mart�n (Parte Francesa)');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'PM', 'San Pedro y Miquel�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VC', 'San Vicente y Las Granadinas');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SH', 'Santa Helena, Ascensi�n y Trist�n de Acu�a');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LC', 'Santa Luc�a');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VA', 'Santa Sede (Ciudad Estado Vaticano)');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ST', 'Santo Tom� y Principe');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SN', 'Senegal');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'RS', 'Serbia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SC', 'Seychelles');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SL', 'Sierra Leona');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SG', 'Singapur');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SX', 'Sint Maarten (Parte Neerlandesa)');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SY', 'Siria, Rep�blica Arabe de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SO', 'Somalia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'LK', 'Sri Lanka');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SZ', 'Suazilandia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ZA', 'Sud�frica');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SS', 'Sud�n del Sur');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SD', 'Sud�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SE', 'Suecia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'CH', 'Suiza');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SR', 'Surinam');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'SJ', 'Svalbard y Jan Mayen');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TH', 'Tailandia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TW', 'Taiw�n, Provincia de China');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TZ', 'Tanzania, Rep�blica Unida de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TJ', 'Tayikist�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'IO', 'Territorio Brit�nico del Oc�ano �ndico');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TF', 'Territorios Australes Franceses');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TL', 'Timor-Leste');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TG', 'Togo');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TK', 'Tokelau');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TO', 'Tonga');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TT', 'Trinidad y Tobago');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TN', 'T�nez');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TM', 'Turkmenist�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TR', 'Turqu�a');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'TV', 'Tuvalu');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UA', 'Ucrania');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UG', 'Uganda');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UY', 'Uruguay');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'UZ', 'Uzbekist�n');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VU', 'Vanuatu');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VE', 'Venezuela, Rep�blica Bolivariana de');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'VN', 'Viet Nam');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'WF', 'Wallis y Futuna');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'YE', 'Yemen');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'DJ', 'Yibuti');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ZM', 'Zambia');
--INSERT INTO build_level001.country(coun_uuid__uniqueidentifier, coun_code__varchar, coun_name__varchar) VALUES(NEWID(), 'ZW', 'Zimbabue');

--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Aguascalientes', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Baja California', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Baja California Sur', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Campeche', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Chiapas', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Chihuahua', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(@stateCoahuilaUUID, 'Coahuila', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Colima', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Distrito Federal', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(@stateDurangoUUID, 'Durango', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Estado de M�xico', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Guanajuato', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Guerrero', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Hidalgo', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Jalisco', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Michoac�n', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Morelos', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Nayarit', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Nuevo Le�n', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Oaxaca', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Puebla', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Quer�taro', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Quintana Roo', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'San Luis Potos�', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Sinaloa', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Sonora', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Tabasco', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Tamaulipas', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Tlaxcala', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Veracruz', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Yucat�n', @countryMexicoUUID);
--INSERT INTO build_level001.[state](stat_uuid__uniqueidentifier, stat_name__varchar, coun_uuid__uniqueidentifier) VALUES(NEWID(), 'Zacatecas', @countryMexicoUUID);

--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Canatl�n', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Canelas', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Coneto de Comonfort', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Cuencam�', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Durango', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'El Oro', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(@cityGomezPalacioUUID, 'G�mez Palacio', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Gral. Sim�n Bo�var', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Guadalupe Victoria', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Guanacev�', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Hidalgo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ind�', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Lerdo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Mapim�', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Mezquital', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nazas', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nombre de Dios', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nuevo Ideal', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ocampo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ot�ez', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'P�nuco de Coronado', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Pe��n Blanco', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Poanas', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Pueblo Nuevo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Rodeo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Bernardo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Dimas', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Juan de Guadalupe', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Juan del R�o', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Luis del Cordero', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Pedro del Gallo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Santa Clara', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Santiago Papasquiaro', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'S�chil', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Tamazula', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Tepehuanes', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Tlahualilo', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Topia', @stateDurangoUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Vicente Guerrero', @stateDurangoUUID);

--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Abasolo', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Acu�a', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Allende', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Arteaga', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Candela', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Casta�os', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Cuatroci�negas', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Escobedo', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Francisco I. Madero', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Frontera', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'General Cepeda', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Guerrero', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Hidalgo', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Jim�nez', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ju�rez', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Lamadrid', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Matamoros', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Monclova', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Morelos', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'M�zquiz', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nadadores', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Nava', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ocampo', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Parras', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Piedras Negras', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Progreso', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Ramos Arizpe', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Sabinas', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Sacramento', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Saltillo', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Buenaventura', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Juan de Sabinas', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'San Pedro', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Sierra Mojada', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(@cityTorreonUUID, 'Torre�n', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Viesca', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Villa Uni�n', @stateCoahuilaUUID);
--INSERT INTO build_level001.city(city_uuid__uniqueidentifier, city_name__varchar, stat_uuid__uniqueidentifier) VALUES(NEWID(), 'Zaragoza', @stateCoahuilaUUID);


----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'AGUASCALIENTES')
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'BAJA CALIFORNIA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'BAJA CALIFORNIA SUR');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'CAMPECHE');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'CHIAPAS');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'CHIHUAHUA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'COAHUILA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'COLIMA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'DISTRITO FEDERAL');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'DURANGO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'GUANAJUATO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'GUERRERO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'HIDALGO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'JALISCO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'M�XICO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'MICHOAC�N');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'MORELOS');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'NAYARIT');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'NUEVO LE�N');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'OAXACA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'PUEBLA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'QUER�TARO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'QUINTANA ROO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'SAN LUIS POTOS�');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'SINALOA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'SONORA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'TABASCO');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'TAMAULIPAS');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'TLAXCALA');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'VERACRUZ');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'YUCAT�N');
----INSERT INTO build_general.[state](stat_uuid, stat_value) VALUES('', 'ZACATECAS');
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUASCALIENTES',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ASIENTOS',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CALVILLO',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COS�O',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JES�S MAR�A',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PABELL�N DE ARTEAGA',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RINC�N DE ROMOS',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO DE LOS ROMO',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEZAL�',  1);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ENSENADA',  2);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MEXICALI',  2);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAYAS DE ROSARITO',  2);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RODOLFO S�NCHEZ T. MANEADERO',  2);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FELIPE',  2);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECATE',  2);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIJUANA',  2);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CABO SAN LUCAS',  3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD CONSTITUCI�N',  3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUERRERO NEGRO', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA MULEG�', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA PAZ', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LORETO', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO ADOLFO L�PEZ MATEOS', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN IGNACIO', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JOS� DEL CABO', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TODOS SANTOS', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA ALBERTO ANDR�S ALVARADO AR�MBURO', 3);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CALKINI', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CANDELARIA', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHAMPOT�N', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DEL CARMEN', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESC�RCEGA', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HECELCHAK�N', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HOPELCH�N', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('POMUCH', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SABANCUY', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO DE CAMPECHE', 4);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACALA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARRIAGA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CACAHOAT�N', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIAPA DE CORZO', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CINTALAPA DE FIGUEROA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMIT�N DE DOM�NGUEZ', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUIXTLA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JIQUIPILAS', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS MARGARITAS', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS ROSAS', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAPASTEPEC', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOTOZINTLA DE MENDOZA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOSINGO', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOZOCOAUTLA DE ESPINOSA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PALENQUE', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PICHUCALCO', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PIJIJIAPAN', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO MADERO SAN BENITO', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('REFORMA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN CRIST�BAL DE LAS CASAS', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAPACHULA DE C�RDOVA Y ORD��EZ', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TONAL�', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXTLA GUTI�RREZ', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VENUSTIANO CARRANZA', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLAFLORES', 5);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BACH�NIVA', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIHUAHUA', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COLONIA AN�HUAC', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUHT�MOC', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('DELICIAS', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HIDALGO DEL PARRAL', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOS� MARIANO JIM�NEZ', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN ALDAMA', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JU�REZ', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MADERA', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MANUEL OJINAGA', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVO CASAS GRANDES', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA ROSAL�A DE CAMARGO', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAUCILLO', 6);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALLENDE', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARTEAGA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CASTA�OS', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD ACU�A', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MELCHOR MUZQUIZ', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUATRO CI�NEGAS DE CARRANZA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRANCISCO I. MADERO CH�VEZ', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRONTERA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MATAMOROS', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MONCLOVA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MORELOS', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NADADORES', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAVA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVA ROSITA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARRAS DE LA FUENTE', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PIEDRAS NEGRAS', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RAMOS ARIZPE', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SABINAS', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALTILLO', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BUENAVENTURA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TORRE�N', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VIESCA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZARAGOZA', 7);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DE ARMER�A', 8);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DE VILLA DE �LVAREZ', 8);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COLIMA', 8);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MANZANILLO', 8);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECOMAN', 8);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('�LVARO OBREG�N', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AZCAPOTZALCO', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BENITO JU�REZ', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COYOAC�N', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAJIMALPA DE MORELOS', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUHT�MOC', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUSTAVO A. MADERO', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IZTACALCO', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IZTAPALAPA', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA MAGDALENA CONTRERAS', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MIGUEL HIDALGO', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MILPA ALTA', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TL�HUAC', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLALPAN', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VENUSTIANO CARRANZA', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XOCHIMILCO', 9);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CANATL�N', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD LERDO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL SALTO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRANCISCO I. MADERO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('G�MEZ PALACIO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NOMBRE DE DIOS', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PE��N BLANCO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN DEL R�O DEL CENTAURO DEL NORTE', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MAR�A DEL ORO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO PAPASQUIARO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VICENTE GUERRERO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VICTORIA DE DURANGO', 10);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ABASOLO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AC�MBARO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APASEO EL ALTO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APASEO EL GRANDE', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CELAYA', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MANUEL DOBLADO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMONFORT', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CORTAZAR', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUER�MARO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('DOCTOR MORA', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EMPALME ESCOBEDO ESCOBEDO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUANAJUATO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAN�MARO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IRAPUATO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JARAL DEL PROGRESO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JER�CUARO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LE�N DE LOS ALDAMA', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARFIL', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOROLE�N', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('P�NJAMO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUR�SIMA DE BUSTOS', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RINC�N DE TAMAYO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ROMITA', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALAMANCA', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALVATIERRA', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN DIEGO DE LA UNI�N', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO DEL RINC�N', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JOS� ITURBIDE', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS DE LA PAZ', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MIGUEL DE ALLENDE', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA CRUZ JUVENTINO ROSAS', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO MARAVAT�O', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SILAO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TARANDACUAO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('URIANGATO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE DE SANTIAGO', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLAGR�N', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YURIRIA', 11);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACAPULCO DE JU�REZ', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARCELIA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATOYAC DE �LVAREZ', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AYUTLA DE LOS LIBRES', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AZOY�', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BUENAVISTA DE CUELLAR', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHILAPA DE �LVAREZ', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHILPANCINGO DE LOS BRAVO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD ALTAMIRANO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD APAXTLA DE CASTREJ�N', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COPALA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COYUCA DE BEN�TEZ', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COYUCA DE CATAL�N', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CRUZ GRANDE', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAJINICUILAPA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUTZAMALA DE PINZ�N', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAMUXTITLAN', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUITZUCO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IGUALA DE LA INDEPENDENCIA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA UNI�N', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARQUELIA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOTITO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OLINAL�', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PETATL�N', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JER�NIMO DE JU�REZ', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS ACATL�N', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS DE LA LOMA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS SAN PEDRO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MARCOS', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAXCO DE ALARC�N', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('T�CPAN DE GALEANA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TELOLOAPAN', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPECOACUILCO DE TRUJANO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIERRA COLORADA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIXTLA DE GUERRERO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLALIXTAQUILLA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAPA DE COMONFORT', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAPEHUALA', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZIHUATANEJO', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZUMPANGO DEL R�O', 12);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACTOPAN', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APAN', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DE FRAY BERNARDINO DE SAHAG�N', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CRUZ AZUL', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUEJUTLA DE REYES', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXMIQUILPAN', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PACHUCA DE SOTO', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO TULANTEPEC', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEAPULCO', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEJI DEL RIO', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIZAYUCA', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAXCOAPAN', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TULA DE ALLENDE', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TULANCINGO', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZIMAPAN', 13);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACATL�N DE JU�REZ', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AHUALULCO DE MERCADO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AJIJIC', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AMECA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ARANDAS', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATOTONILCO EL ALTO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AUTL�N DE NAVARRO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHAPALA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIHUATL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD GUZM�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COCULA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COLOTL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL GRULLO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL QUINCE SAN JOS� EL QUINCE', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ETZATL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUADALAJARA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUEJUQUILLA EL ALTO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALOSTOTITL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JAMAY', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOCOTEPEC', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA BARCA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA RESOLANA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAGOS DE MORENO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS PINTITAS', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAGDALENA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOTL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PONCITL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO VALLARTA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN DIEGO DE ALEJANDR�A', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN IGNACIO CERRO GORDO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JOS� EL VERDE EL VERDE', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN DE LOS LAGOS', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JULI�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MIGUEL EL ALTO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAYULA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TALA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TALPA DE ALLENDE', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMAZULA DE GORDIANO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECALITL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEOCALTICHE', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPATITL�N DE MORELOS', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEQUILA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAJOMULCO DE Z��IGA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAQUEPAQUE', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TONAL�', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TOTOTL�N', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXPAN', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('UNI�N DE SAN ANTONIO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE DE GUADALUPE', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA CORONA', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA HIDALGO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YAHUALICA DE GONZ�LEZ GALLO', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACOALCO DE TORRES', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZAPOPAN', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZAPOTILTIC', 14);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALMOLOYA DE JU�REZ', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AMATEPEC', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CAPULHUAC', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHALCO DE D�AZ COVARRUBIAS', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHICONCUAC', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIMALHUAC�N', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD ADOLFO L�PEZ MATEOS', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD NEZAHUALCOYOTL', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COACALCO DE BERRIOZABAL', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTITL�N', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTITL�N IZCALLI', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ECATEPEC DE MORELOS', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUIXQUILUCAN DE DEGOLLADO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXTAPALUCA', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUCHITEPEC DE MARIANO RIVA PALACIO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOS REYES ACAQUILPAN LA PAZ', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MELCHOR OCAMPO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('METEPEC', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAUCALPAN DE JU�REZ', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOYOACAC', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MATEO ATENCO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN VICENTE CHICOLOAPAN DE JU�REZ', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MARIA TULTEPEC', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECAMAC DE FELIPE VILLANUEVA', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEJUPILCO DE HIDALGO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPOTZOTL�N', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEQUIXQUIAC', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEXCOCO DE MORA', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLALNEPANTLA DE BAZ', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TOLUCA DE LERDO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TULTITL�N DE MARIANO ESCOBEDO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE DE CHALCO SOLIDARIDAD', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA NICOL�S ROMERO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XONACATL�N', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZUMPANGO', 15);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APATZING�N DE LA CONSTITUCI�N', 16);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD HIDALGO', 16);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD L�ZARO C�RDENAS', 16);
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COTIJA DE LA PAZ',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUITZEO DEL PORVENIR',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA ZIT�CUARO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUETAMO DE N��EZ',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JACONA DE PLANCARTE',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JIQUILPAN DE JU�REZ',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA PIEDAD DE CABADAS',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS GUACAMAYAS',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOS REYES DE SALGADO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARAVAT�O DE OCAMPO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MORELIA',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVA ITALIA DE RUIZ',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARACHO DE VERDUZCO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('P�TZCUARO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PURU�NDIRO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAHUAYO DE MORELOS',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAC�MBARO DE CODALLOS',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TANGANC�CUARO DE ARISTA',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXPAN',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('URUAPAN',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YUR�CUARO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACAPU',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZAMORA DE HIDALGO',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZINAP�CUARO DE FIGUEROA',16); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTLA CUAUTLA DE MORELOS',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUERNAVACA',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GALEANA',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOJUTLA',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUENTE DE IXTLA',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA ROSA TREINTA',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAQUILTENANGO',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACATEPEC DE HIDALGO',17); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACAPONETA',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AHUACATL�N',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BUCER�AS',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMPOSTELA',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRANCISCO I. MADERO (PUGA)',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXTL�N DEL R�O',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALA',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA PE�ITA DE JALTEMBA',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS VARAS',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RUIZ',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BLAS',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO LAGUNILLAS',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO IXCUINTLA',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECUALA',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPIC',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TUXPAN',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA HIDALGO EL NUEVO',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XALISCO',18); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AN�HUAC',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CADEREYTA JIM�NEZ',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CI�NEGA DE FLORES',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD APODACA',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD BENITO JU�REZ',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD GENERAL ESCOBEDO',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD SABINAS HIDALGO',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD SANTA CATARINA',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('DOCTOR ARROYO',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL CERCADO',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GARC�A',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUADALUPE',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUALAHUISES',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LINARES',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MONTEMORELOS',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MONTERREY',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN NICOL�S DE LOS GARZA',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO GARZA GARC�A',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO',19); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ASUNCI�N NOCHIXTL�N',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BAHIAS DE HUATULCO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHAHUITES',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD IXTEPEC',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSOLAPA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUIL�PAM DE GUERRERO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL CAMAR�N',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL ROSARIO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE EJUTLA DE CRESPO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE HUAJUAPAN DE LE�N',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE TLAXIACO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUCHIT�N (JUCHIT�N DE ZARAGOZA)',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAGUNAS',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOMA BONITA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MARISCALA DE JU�REZ',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAT�AS ROMERO AVENDA�O',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MIAHUATL�N DE PORFIRIO D�AZ',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NATIVIDAD',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OAXACA DE JU�REZ',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OCOTL�N DE MORELOS',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO ESCONDIDO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUTLA VILLA DE GUERRERO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('R�O GRANDE O PIEDRA PARADA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALINA CRUZ',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN ANTONIO DE LA CAL',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BLAS ATEMPA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FELIPE JALAPA DE D�AZ',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO IXHUAT�N',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FRANCISCO TELIXTLAHUACA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA CUICATL�N',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA LO DE SOTO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA TUXTEPEC',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN BAUTISTA VALLE NACIONAL',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN CACAHUATEPEC',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MIGUEL EL GRANDE',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PABLO HUITZO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PABLO VILLA DE MITLA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO MIXTEPEC DTO. 22',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO POCHUTLA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO TAPANATEPEC',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO TOTOLAPA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN SEBASTI�N TECOMAXTLAHUACA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA CRUZ ITUNDUJIA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA LUCIA DEL CAMINO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MAR�A HUATULCO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO JAMILTEPEC',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO JUXTLAHUACA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO PINOTEPA NACIONAL',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO SUCHILQUITONGO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTO DOMINGO TEHUANTEPEC',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEOTITL�N DE FLORES MAG�N',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLACOLULA DE MATAMOROS',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('UNI�N HIDALGO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VICENTE CAMALOTE',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE TAMAZUL�PAM DEL PROGRESO',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE ZAACHILA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA SOLA DE VEGA',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZIMATL�N DE �LVAREZ',20); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACATL�N DE OSORIO',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AMOZOC',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATLIXCO',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD SERD�N',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUAUTLANCINGO',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAUCHINANGO',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IZ�CAR DE MATAMOROS',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUEBLA HEROICA PUEBLA',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN ANDR�S CHOLULA',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN MART�N TEXMELUCAN DE LABASTIDA',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN PEDRO CHOLULA',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TECAMACHALCO',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEHUACAN',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEPEACA',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEZIUTLAN',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XICOTEPEC',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACATL�N',21); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL PUEBLITO',22); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('QUER�TARO',22); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN JUAN DEL RIO',22); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BACALAR',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CANC�N',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHETUMAL',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COZUMEL',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FELIPE CARRILLO PUERTO',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ISLA MUJERES',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('KANTUNILK�N',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAYA DEL CARMEN',23); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('C�RDENAS',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CEDRAL',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CERRITOS',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHARCAS',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD DEL MA�Z',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD VALLES',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('�BANO',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL NARANJO',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRACCI�N EL REFUGIO',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MATEHUALA',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('RIOVERDE',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SALINAS DE HIDALGO',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS POTOS�',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTA MAR�A DEL R�O',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOLEDAD DE GRACIANO S�NCHEZ',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMASOPO',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMAZUNCHALE',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMUIN',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIERRA NUEVA',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE REYES',24); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUARUTO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AHOME',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ANGOSTURA',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHOIX',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSAL�',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CULIAC�N ROSALES',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL ROSARIO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESCUINAPA DE HIDALGO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESTACI�N NARANJO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUAM�CHIL',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUASAVE',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HIGUERA DE ZARAGOZA',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LA CRUZ',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LIC. BENITO JU�REZ CAMPO GOBIERNO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LOS MOCHIS',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAZATL�N',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOCORITO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAVOLATO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('QUILA',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN BLAS',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN IGNACIO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SINALOA DE LEYVA',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TOPOLOBAMPO',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA UNI�N',25); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUA PRIETA',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD OBREG�N',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EMPALME',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HERMOSILLO',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CABORCA',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA CIUDAD DE CANANEA',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA GUAYMAS',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA NOGALES',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUATABAMPO',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MAGDALENA DE KINO',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NAVOJOA',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PUERTO PE�ASCO',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN LUIS R�O COLORADO',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SONOITA',26); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('C�RDENAS',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COMALCALCO',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUNDUAC�N',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EMILIANO ZAPATA',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRONTERA',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUIMANGUILLO',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALPA DE M�NDEZ',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MACUSPANA',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARA�SO',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEAPA',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TENOSIQUE DE PINO SU�REZ',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLAHERMOSA',27); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALTAMIRA',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD CAMARGO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD GUSTAVO D�AZ ORDAZ',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MADERO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MANTE',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD MIGUEL ALEM�N',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD R�O BRAVO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD TULA',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD VICTORIA',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ESTACI�N MANUEL �RSULO GALV�N',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GONZ�LEZ',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HEROICA MATAMOROS',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JAUMAVE',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVA CIUDAD GUERRERO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NUEVO LAREDO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('REYNOSA',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN FERNANDO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOTO LA MARINA',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMPICO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLE HERMOSO',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XICOT�NCATL',28); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('APIZACO',29); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CALPULALPAN',29); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CHIAUTEMPAN',29); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAMANTLA',29); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAXCALA TLAXCALA DE XICOTENCATL',29); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA VICENTE GUERRERO',29); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ACAYUCAN',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('AGUA DULCE',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALTOTONGA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ALVARADO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('�NGEL R. CABADA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ATOYAC',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BANDERILLA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('BOCA DEL R�O',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CARLOS A. CARRILLO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CATEMACO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CAZONES DE HERRERA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CERRO AZUL',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COATEPEC',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COATZACOALCOS',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COATZINTLA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('C�RDOBA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSAMALOAPAN',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('COSOLEACAQUE',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUICHAPA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CUITL�HUAC',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('EL HIGO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FORT�N DE LAS FLORES',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GENERAL MIGUEL ALEM�N POTRERO NUEVO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUTI�RREZ ZAMORA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUATUSCO DE CHICUELLAR',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUAYACOCOTLA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('HUILOAPAN DE CUAUHT�MOC',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ISLA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('IXTACZOQUITL�N',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('J�LTIPAN DE MORELOS',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JOS� CARDEL',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN D�AZ COVARRUBIAS',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN RODR�GUEZ CLARA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LAS CHOAPAS',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LERDO DE TEJADA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MINATITL�N',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NARANJOS',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NOGALES',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ORIZABA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('P�NUCO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PAPANTLA DE OLARTE',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PARAJE NUEVO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PASO DE OVEJAS',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PASO DEL MACHO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAT�N S�NCHEZ',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('PLAYA VICENTE',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('POZA RICA DE HIDALGO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('R�O BLANCO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN ANDR�S TUXTLA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SAN RAFAEL',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SANTIAGO TUXTLA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SIHUAPAN',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOLEDAD DE DOBLADO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TAMPICO ALTO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TANTOYUCA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEMPOAL DE S�NCHEZ',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TEZONAPA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIERRA BLANCA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIHUATL�N',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLACOJALPAN',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TLAPACOYAN',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TRES VALLES',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('T�XPAM DE RODR�GUEZ CANO',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VERACRUZ',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('XALAPA-ENR�QUEZ',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('YECUATLA',30); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('M�RIDA',31); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOTUL DE CARRILLO PUERTO',31); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TICUL',31); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('TIZIM�N',31); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALLADOLID',31); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('CIUDAD CUAUHT�MOC',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('FRESNILLO',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('GUADALUPE',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JALPA',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JEREZ DE GARC�A SALINAS',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('JUAN ALDAMA',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LORETO',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('LUIS MOYA',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('MOYAHUA DE ESTRADA',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('NOCHISTL�N DE MEJ�A',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('OJOCALIENTE',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('R�O GRANDE',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('SOMBRERETE',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VALPARA�SO',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('V�CTOR ROSALES',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA DE COS',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLA HIDALGO',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('VILLANUEVA',32); 
----INSERT INTO build_general.city(city_value, stat_uuid) VALUES('ZACATECAS',32);


--INSERT INTO build_level002.[date](date_uuid__uniqueidentifier, date_value__date) VALUES(@dateUUID, '2000-01-01');

--INSERT INTO build_level002.[session](sess_uuid__uniqueidentifier, sess_starttime__time, sess_lastactivity__time, sess_ipbatch01__tinyint, sess_ipbatch02__tinyint, sess_ipbatch03__tinyint, sess_ipbatch04__tinyint, sess_extradata__varchar, date_uuid__uniqueidentifier, user_uuid_created__uniqueidentifier) VALUES(@sessionStraadUUID, '13:00:00', '13:00:00', NULL, NULL, NULL, NULL, NULL, @dateUUID, @user001UUID);

--INSERT INTO build_level001.[user](user_uuid__uniqueidentifier, user_username__varchar, user_email__varchar, user_cellphone__varchar, user_password__varbinary, user_firstname__varchar, user_lastname__varchar, user_roleaccess__tinyint, user_extradata__varchar, reso_uuid_picture__uniqueidentifier, date_uuid_birthdate__uniqueidentifier, city_uuid__uniqueidentifier, sess_uuid_used__uniqueidentifier, sess_uuid_created__uniqueidentifier, user_uuid_root__uniqueidentifier, sess_uuid_deleted__uniqueidentifier) VALUES(
--	@user001UUID, 'admin', NULL, NULL, 
--	HASHBYTES('sha2_512', HASHBYTES('sha2_256', 'admin')), 
--	ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3','Administrator'), 
--	ENCRYPTBYPASSPHRASE('d-f5]8T.x6_[s3', 'Administrator'), 
--	1, NULL, NULL, NULL, NULL, NULL, @sessionStraadUUID, NULL, NULL);

----EXEC build_level001.proc_user_insert(@user001UUID, 'admin', NULL, NULL, 'admin', 'Administrator', 'Administrator', 1, NULL, NULL, NULL, NULL, NULL, @sessionStraadUUID, NULL, NULL);

--GO

--ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser373134 FOREIGN KEY (sess_uuid_created__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
--ALTER TABLE build_level001.preference ADD CONSTRAINT FKpreference267585 FOREIGN KEY (pref_uuid_root__uniqueidentifier) REFERENCES build_level001.preference (pref_uuid__uniqueidentifier);
--ALTER TABLE build_level001.[state] ADD CONSTRAINT FKstate183298 FOREIGN KEY (coun_uuid__uniqueidentifier) REFERENCES build_level001.country (coun_uuid__uniqueidentifier);
--ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser138147 FOREIGN KEY (user_uuid_root__uniqueidentifier) REFERENCES build_level001.[user] (user_uuid__uniqueidentifier);
--ALTER TABLE build_level001.city ADD CONSTRAINT FKcity970084 FOREIGN KEY (stat_uuid__uniqueidentifier) REFERENCES build_level001.[state] (stat_uuid__uniqueidentifier);
--ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser85024 FOREIGN KEY (city_uuid__uniqueidentifier) REFERENCES build_level001.city (city_uuid__uniqueidentifier);
--ALTER TABLE build_level002.[session] ADD CONSTRAINT FKsession868286 FOREIGN KEY (user_uuid_created__uniqueidentifier) REFERENCES build_level001.[user] (user_uuid__uniqueidentifier);
--ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser377146 FOREIGN KEY (sess_uuid_deleted__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
--ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser515892 FOREIGN KEY (sess_uuid_used__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
--ALTER TABLE build_level002.[session] ADD CONSTRAINT FKsession541289 FOREIGN KEY (date_uuid__uniqueidentifier) REFERENCES build_level002.[date] (date_uuid__uniqueidentifier);
--ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser951719 FOREIGN KEY (reso_uuid_picture__uniqueidentifier) REFERENCES build_level002.[resource] (reso_uuid__uniqueidentifier);
--ALTER TABLE build_level001.preference ADD CONSTRAINT FKpreference627068 FOREIGN KEY (user_uuid__uniqueidentifier) REFERENCES build_level001.[user] (user_uuid__uniqueidentifier);
--ALTER TABLE build_level001.preference ADD CONSTRAINT FKpreference59225 FOREIGN KEY (sess_uuid_created__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
--ALTER TABLE build_level001.[user] ADD CONSTRAINT FKuser954464 FOREIGN KEY (date_uuid_birthdate__uniqueidentifier) REFERENCES build_level002.[date] (date_uuid__uniqueidentifier);
--ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc68501 FOREIGN KEY (city_uuid__uniqueidentifier) REFERENCES build_level001.city (city_uuid__uniqueidentifier);
--ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc633898 FOREIGN KEY (prco_uuid_root__uniqueidentifier) REFERENCES build_level003.principalcompany (prco_uuid__uniqueidentifier);
--ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc389657 FOREIGN KEY (sess_uuid_created__uniqueidentifier) REFERENCES build_level002.[session] (sess_uuid__uniqueidentifier);
--ALTER TABLE build_level003.[version] ADD CONSTRAINT FKversion840117 FOREIGN KEY (date_uuid__uniqueidentifier) REFERENCES build_level002.[date] (date_uuid__uniqueidentifier);
--ALTER TABLE build_level003.principalcompany ADD CONSTRAINT FKprincipalc114089 FOREIGN KEY (reso_uuid_logo__uniqueidentifier) REFERENCES build_level002.[resource] (reso_uuid__uniqueidentifier);

--GO

--CREATE INDEX state_stat_name__varchar ON build_level001.[state] (stat_name__varchar);
--CREATE UNIQUE INDEX country_coun_code__varchar ON build_level001.country (coun_code__varchar);
--CREATE INDEX user_user_username__varchar ON build_level001.[user] (user_username__varchar);
--CREATE INDEX user_user_email__varchar ON build_level001.[user] (user_email__varchar);
--CREATE INDEX user_user_cellphone__varchar ON build_level001.[user] (user_cellphone__varchar);
--CREATE INDEX city_city_name__varchar ON build_level001.city (city_name__varchar);
--CREATE UNIQUE INDEX date_date_value__date ON build_level002.[date] (date_value__date);
--CREATE INDEX resource_reso_name__varchar ON build_level002.[resource] (reso_name__varchar);


