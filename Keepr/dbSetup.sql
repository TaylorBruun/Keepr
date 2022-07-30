CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT(255) AUTO_INCREMENT NOT NULL PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(255) NOT NULL,
        img VARCHAR(255) NOT NULL,
        views INT DEFAULT 0 NOT NULL,
        kept INT DEFAULT 0 NOT NULL,
        shares INT DEFAULT 0 NOT NULL,
        creatorId VARCHAR(255),
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT(255) AUTO_INCREMENT NOT NULL PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(255) NOT NULL,
        isPrivate TINYINT NOT NULL,
        creatorId VARCHAR(255),
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT(255) AUTO_INCREMENT NOT NULL PRIMARY KEY,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        creatorId VARCHAR(255),
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id),
        FOREIGN KEY (keepId) REFERENCES keeps(id)
    ) default charset utf8 COMMENT '';

SELECT keeps.*, accounts.*, vaultKeeps.id from keeps JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id JOIN accounts ON accounts.id = keeps.creatorId WHERE vaultKeeps.vaultId = 28;