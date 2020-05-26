delete from [t_index_history];
delete from [m_license];
delete from sqlite_sequence where name='t_index_history';
vacuum;