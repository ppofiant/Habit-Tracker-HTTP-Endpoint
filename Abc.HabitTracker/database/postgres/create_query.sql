create table "badge"
(
	id uuid primary key,
	name varchar(100) not null,
	description varchar(255) not null,
	user_id uuid not null,
	habit_id uuid not null,
	created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMPTZ,
	deleted_at TIMESTAMPTZ
	FOREIGN KEY(habit_id) references "habit" (user_id)
);

create table "habit"
(
	id uuid primary key,
	name varchar(100) not null,
	user_id uuid not null,
	--replace '--' for creating table
	--days_off text[] not null,
	created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMPTZ,
	deleted_at TIMESTAMPTZ
);

create table "user"
(
	id uuid primary key,
	name varchar(100) not null
);

create table "logs"
(
	id uuid primary key,
	habit_id uuid not null,
	user_id uuid not null,
	created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMPTZ,
	FOREIGN KEY(habit_id) references "habit" (id)
);

create table "logs_snapshot"
(
	id uuid primary key,
	habit_id uuid not null,
	user_id uuid not null,
	last_logs_id uuid not null,
	last_logs_created_at TIMESTAMPTZ not null,
	created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (habit_id) references "habit" (id),
	FOREIGN KEY (last_logs_id) references "logs" (id)
);