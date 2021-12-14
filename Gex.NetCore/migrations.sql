CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `migration_id` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `product_version` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `pk___ef_migrations_history` PRIMARY KEY (`migration_id`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `comisiones` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `nombre` varchar(100) CHARACTER SET utf8mb4 NULL,
        `ciclo_lectivo` int NOT NULL,
        `updated_at` datetime(6) NOT NULL,
        `created_at` datetime(6) NOT NULL,
        `estado` int NOT NULL,
        CONSTRAINT `pk_comisiones` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `materias` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `nombre` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `tipo` int NOT NULL,
        `updated_at` datetime(6) NOT NULL,
        `created_at` datetime(6) NOT NULL,
        `estado` int NOT NULL,
        CONSTRAINT `pk_materias` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `usuarios` (
        `id` int NOT NULL AUTO_INCREMENT,
        `email_verified_at` datetime(6) NULL,
        `password` longtext CHARACTER SET utf8mb4 NULL,
        `salt` longtext CHARACTER SET utf8mb4 NULL,
        `remember_token` longtext CHARACTER SET utf8mb4 NULL,
        `profile_photo_path` longtext CHARACTER SET utf8mb4 NULL,
        `dni` bigint NULL,
        `last_name` longtext CHARACTER SET utf8mb4 NULL,
        `tipo` int NOT NULL,
        `user_name` longtext CHARACTER SET utf8mb4 NULL,
        `normalized_user_name` longtext CHARACTER SET utf8mb4 NULL,
        `email` longtext CHARACTER SET utf8mb4 NULL,
        `normalized_email` longtext CHARACTER SET utf8mb4 NULL,
        `email_confirmed` tinyint(1) NOT NULL,
        `password_hash` longtext CHARACTER SET utf8mb4 NULL,
        `security_stamp` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` longtext CHARACTER SET utf8mb4 NULL,
        `phone_number` longtext CHARACTER SET utf8mb4 NULL,
        `phone_number_confirmed` tinyint(1) NOT NULL,
        `two_factor_enabled` tinyint(1) NOT NULL,
        `lockout_end` datetime(6) NULL,
        `lockout_enabled` tinyint(1) NOT NULL,
        `access_failed_count` int NOT NULL,
        CONSTRAINT `pk_usuarios` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `cursadas` (
        `id` int NOT NULL AUTO_INCREMENT,
        `comision_id` bigint NULL,
        `materia_id` bigint NULL,
        `fecha` datetime(6) NULL,
        CONSTRAINT `pk_cursadas` PRIMARY KEY (`id`),
        CONSTRAINT `fk_cursadas_comisiones_comision_id` FOREIGN KEY (`comision_id`) REFERENCES `comisiones` (`id`),
        CONSTRAINT `fk_cursadas_materias_materia_id` FOREIGN KEY (`materia_id`) REFERENCES `materias` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `examenes` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `materia_id` bigint NULL,
        `tipo` int NOT NULL,
        `nota_regular` int NOT NULL,
        `nota_promo` int NOT NULL,
        `recuperatorio` tinyint(1) NOT NULL,
        `updated_at` datetime(6) NOT NULL,
        `created_at` datetime(6) NOT NULL,
        `estado` int NOT NULL,
        CONSTRAINT `pk_examenes` PRIMARY KEY (`id`),
        CONSTRAINT `fk_examenes_materias_materia_id` FOREIGN KEY (`materia_id`) REFERENCES `materias` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `inscripciones_comisiones` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `fecha` datetime(6) NOT NULL,
        `alumno_id` int NULL,
        `comision_id` bigint NULL,
        CONSTRAINT `pk_inscripciones_comisiones` PRIMARY KEY (`id`),
        CONSTRAINT `fk_inscripciones_comisiones_comisiones_comision_id` FOREIGN KEY (`comision_id`) REFERENCES `comisiones` (`id`),
        CONSTRAINT `fk_inscripciones_comisiones_usuarios_alumno_id` FOREIGN KEY (`alumno_id`) REFERENCES `usuarios` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `mesas_examen` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `fecha` datetime(6) NULL,
        `mostrar_respuestas` tinyint(1) NOT NULL,
        `duracion` int NOT NULL,
        `examen_id` bigint NULL,
        `profesor_id` int NULL,
        `updated_at` datetime(6) NOT NULL,
        `created_at` datetime(6) NOT NULL,
        `estado` int NOT NULL,
        CONSTRAINT `pk_mesas_examen` PRIMARY KEY (`id`),
        CONSTRAINT `fk_mesas_examen_examenes_examen_id` FOREIGN KEY (`examen_id`) REFERENCES `examenes` (`id`),
        CONSTRAINT `fk_mesas_examen_usuarios_profesor_id` FOREIGN KEY (`profesor_id`) REFERENCES `usuarios` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `preguntas` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `tema` longtext CHARACTER SET utf8mb4 NULL,
        `descripcion` longtext CHARACTER SET utf8mb4 NULL,
        `tipo` int NOT NULL,
        `examen_id` bigint NULL,
        `materia_id` bigint NULL,
        `updated_at` datetime(6) NOT NULL,
        `created_at` datetime(6) NOT NULL,
        `estado` int NOT NULL,
        CONSTRAINT `pk_preguntas` PRIMARY KEY (`id`),
        CONSTRAINT `fk_preguntas_examenes_examen_id` FOREIGN KEY (`examen_id`) REFERENCES `examenes` (`id`),
        CONSTRAINT `fk_preguntas_materias_materia_id` FOREIGN KEY (`materia_id`) REFERENCES `materias` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `inscripciones_mesas` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `nota` int NOT NULL,
        `condicion` int NOT NULL,
        `alumno_id` int NULL,
        `mesa_examen_id` bigint NULL,
        CONSTRAINT `pk_inscripciones_mesas` PRIMARY KEY (`id`),
        CONSTRAINT `fk_inscripciones_mesas_mesas_examen_mesa_examen_id` FOREIGN KEY (`mesa_examen_id`) REFERENCES `mesas_examen` (`id`),
        CONSTRAINT `fk_inscripciones_mesas_usuarios_alumno_id` FOREIGN KEY (`alumno_id`) REFERENCES `usuarios` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `preguntas_examen` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `mesa_examen_id` bigint NULL,
        `pregunta_id` bigint NULL,
        `puntos` int NOT NULL,
        CONSTRAINT `pk_preguntas_examen` PRIMARY KEY (`id`),
        CONSTRAINT `fk_preguntas_examen_mesas_examen_mesa_examen_id` FOREIGN KEY (`mesa_examen_id`) REFERENCES `mesas_examen` (`id`),
        CONSTRAINT `fk_preguntas_examen_preguntas_pregunta_id` FOREIGN KEY (`pregunta_id`) REFERENCES `preguntas` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `respuestas` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `valor` longtext CHARACTER SET utf8mb4 NULL,
        `correcto` tinyint(1) NULL,
        `pregunta_id` bigint NOT NULL,
        CONSTRAINT `pk_respuestas` PRIMARY KEY (`id`),
        CONSTRAINT `fk_respuestas_preguntas_pregunta_id` FOREIGN KEY (`pregunta_id`) REFERENCES `preguntas` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE TABLE `respuestas_alumnos` (
        `id` bigint NOT NULL AUTO_INCREMENT,
        `alumno_id` int NULL,
        `examen_id` bigint NULL,
        `respuesta_id` bigint NULL,
        `valor` longtext CHARACTER SET utf8mb4 NULL,
        `correcto` tinyint(1) NULL,
        CONSTRAINT `pk_respuestas_alumnos` PRIMARY KEY (`id`),
        CONSTRAINT `fk_respuestas_alumnos_mesas_examen_examen_id` FOREIGN KEY (`examen_id`) REFERENCES `mesas_examen` (`id`),
        CONSTRAINT `fk_respuestas_alumnos_respuestas_respuesta_id` FOREIGN KEY (`respuesta_id`) REFERENCES `respuestas` (`id`),
        CONSTRAINT `fk_respuestas_alumnos_usuarios_alumno_id` FOREIGN KEY (`alumno_id`) REFERENCES `usuarios` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE UNIQUE INDEX `ix_comisiones_nombre_ciclo_lectivo` ON `comisiones` (`nombre`, `ciclo_lectivo`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_cursadas_comision_id` ON `cursadas` (`comision_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_cursadas_materia_id` ON `cursadas` (`materia_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_examenes_materia_id` ON `examenes` (`materia_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_inscripciones_comisiones_alumno_id` ON `inscripciones_comisiones` (`alumno_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_inscripciones_comisiones_comision_id` ON `inscripciones_comisiones` (`comision_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_inscripciones_mesas_alumno_id` ON `inscripciones_mesas` (`alumno_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_inscripciones_mesas_mesa_examen_id` ON `inscripciones_mesas` (`mesa_examen_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE UNIQUE INDEX `ix_materias_nombre` ON `materias` (`nombre`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_mesas_examen_examen_id` ON `mesas_examen` (`examen_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_mesas_examen_profesor_id` ON `mesas_examen` (`profesor_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_preguntas_examen_id` ON `preguntas` (`examen_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_preguntas_materia_id` ON `preguntas` (`materia_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_preguntas_examen_mesa_examen_id` ON `preguntas_examen` (`mesa_examen_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_preguntas_examen_pregunta_id` ON `preguntas_examen` (`pregunta_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_respuestas_pregunta_id` ON `respuestas` (`pregunta_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_respuestas_alumnos_alumno_id` ON `respuestas_alumnos` (`alumno_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_respuestas_alumnos_examen_id` ON `respuestas_alumnos` (`examen_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    CREATE INDEX `ix_respuestas_alumnos_respuesta_id` ON `respuestas_alumnos` (`respuesta_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211212201952_InitialCreate') THEN

    INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
    VALUES ('20211212201952_InitialCreate', '6.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `mesas_examen` DROP FOREIGN KEY `fk_mesas_examen_examenes_examen_id`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `mesas_examen` DROP FOREIGN KEY `fk_mesas_examen_usuarios_profesor_id`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `mesas_examen` DROP INDEX `ix_mesas_examen_examen_id`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `mesas_examen` DROP INDEX `ix_mesas_examen_profesor_id`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `usuarios` DROP COLUMN `dni`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `usuarios` ADD `birth_date` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `mesas_examen` MODIFY COLUMN `profesor_id` bigint NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    ALTER TABLE `mesas_examen` MODIFY COLUMN `examen_id` bigint NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214114940_Usuario_BirthDate') THEN

    INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
    VALUES ('20211214114940_Usuario_BirthDate', '6.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214122431_UsuarioFirstName') THEN

    ALTER TABLE `usuarios` ADD `first_name` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214122431_UsuarioFirstName') THEN

    INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
    VALUES ('20211214122431_UsuarioFirstName', '6.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    ALTER TABLE `mesas_examen` MODIFY COLUMN `profesor_id` int NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    ALTER TABLE `mesas_examen` MODIFY COLUMN `examen_id` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    ALTER TABLE `inscripciones_mesas` ADD `created_at` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    ALTER TABLE `inscripciones_mesas` ADD `estado` int NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    ALTER TABLE `inscripciones_mesas` ADD `updated_at` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    CREATE INDEX `ix_mesas_examen_examen_id` ON `mesas_examen` (`examen_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    CREATE INDEX `ix_mesas_examen_profesor_id` ON `mesas_examen` (`profesor_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    ALTER TABLE `mesas_examen` ADD CONSTRAINT `fk_mesas_examen_examenes_examen_id` FOREIGN KEY (`examen_id`) REFERENCES `examenes` (`id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    ALTER TABLE `mesas_examen` ADD CONSTRAINT `fk_mesas_examen_usuarios_profesor_id` FOREIGN KEY (`profesor_id`) REFERENCES `usuarios` (`id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20211214143235_UsuairoProfesor') THEN

    INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
    VALUES ('20211214143235_UsuairoProfesor', '6.0.0');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

