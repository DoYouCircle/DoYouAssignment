CREATE TABLE IF NOT EXISTS `courses` (
  `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  `name` VARCHAR(45) NULL,
  `description` MEDIUMTEXT NULL,
  `semester` VARCHAR(45) NULL,
  `subject` VARCHAR(45) NULL
);

CREATE TABLE IF NOT EXISTS `assignment_groups` (
  `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  `course_id` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  `category` VARCHAR(45) NULL,
  `description` MEDIUMTEXT NULL,
  CONSTRAINT `fk_assignments_courses`
    FOREIGN KEY (`course_id`)
    REFERENCES `courses` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
);

CREATE TABLE IF NOT EXISTS `assignments` (
  `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  `assignment_group_id` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  `due_date` DATETIME NULL,
  `score` INT NULL,
  `max_points` INT NULL,
  `comment` MEDIUMTEXT NULL,
  `submitted` BINARY NULL DEFAULT FALSE,
  CONSTRAINT `fk_tasks_assignments1`
    FOREIGN KEY (`assignment_group_id`)
    REFERENCES `assignment_groups` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
);

CREATE TABLE IF NOT EXISTS `conditions` (
  `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  `assignment_group_id` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  `type` INT NULL,
  `property` FLOAT NULL,
  CONSTRAINT `fk_conditions_assignments1`
    FOREIGN KEY (`assignment_group_id`)
    REFERENCES `assignment_groups` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
);
