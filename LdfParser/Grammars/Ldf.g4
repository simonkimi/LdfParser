grammar Ldf;

start: ldf_body EOF;

ldf_body: ( header_description_file 
          | header_protocol_version
          | header_language_version
          | header_speed
          | header_channel_name
          | nodes
          | signals
          | diagnostic_signals
          | frames
          | diagnostic_frames
          | node_attributes
          | schedule_tables
          | signal_encoding_types
          | signal_representation
          )*;


// Header Elements
header_description_file: 'LIN_description_file' SEMI;
header_protocol_version: 'LIN_protocol_version' EQ ldf_version SEMI;
header_language_version: 'LIN_language_version' EQ ldf_version SEMI;
header_speed: 'LIN_speed' EQ ldf_float 'kbps' SEMI;
header_channel_name: 'Channel_name' EQ ldf_str SEMI;

// Node Elements
nodes: 'Nodes' '{' node_master? node_slaves? '}';
node_master: 'Master' ':' ldf_name ',' ldf_float 'ms' ',' ldf_float 'ms' (',' ldf_int 'bits' ',' ldf_float '%')? SEMI;
node_slaves: 'Slaves' ':' ldf_name (',' ldf_name)* SEMI;

// Signals
signals: 'Signals' '{' signal_definition* '}';
signal_definition: ldf_name ':' ldf_int ',' ldf_int ',' ldf_name (',' ldf_name)* SEMI;

// Diagnostic_signals
diagnostic_signals: 'Diagnostic_signals' '{' diagnostic_signal_definition* '}';
diagnostic_signal_definition: ldf_name ':' ldf_int ',' ldf_int SEMI;

// Frames
frames: 'Frames' '{' frame_definition* '}';
frame_definition: ldf_name ':' ldf_int ',' ldf_name ',' ldf_int '{' frame_signal* '}';
frame_signal: ldf_name ',' ldf_int SEMI;

// Diagnostic_frames
diagnostic_frames: 'Diagnostic_frames' '{' diagnostic_frame_definition* '}';
diagnostic_frame_definition: ldf_name ':' ldf_int '{' diagnostic_frame_signals* '}';
diagnostic_frame_signals: ldf_name ',' ldf_int SEMI;

// Node_attributes
node_attributes: 'Node_attributes' '{' node_attribute* '}';
node_attribute: ldf_name '{' node_definition* '}';
node_definition: node_definition_protocol | node_definition_configured_nad | node_definition_initial_nad | node_definition_product_id | node_definition_response_error | node_definition_configurable_frames;
node_definition_protocol: 'LIN_protocol' EQ ldf_version SEMI;
node_definition_configured_nad: 'configured_NAD' EQ ldf_int SEMI;
node_definition_initial_nad: 'initial_NAD' EQ ldf_int SEMI;
node_definition_product_id: 'product_id' EQ ldf_int ',' ldf_int ',' ldf_int SEMI;
node_definition_response_error: 'response_error' EQ ldf_name SEMI;
node_definition_configurable_frames: 'configurable_frames' '{' node_definition_configurable_frame* '}';
node_definition_configurable_frame: ldf_name SEMI;

// Schedule_tables
schedule_tables: 'Schedule_tables' '{' schedule_table_definition* '}';
schedule_table_definition: ldf_name '{' schedule_table_entry* '}';
schedule_table_entry: ldf_name 'delay' ldf_float 'ms' SEMI;

// Signal_encoding_types
signal_encoding_types: 'Signal_encoding_types' '{' signal_encoding_type* '}';
signal_encoding_type: ldf_name '{' signal_encoding_type_value* '}';
signal_encoding_type_value: signal_encoding_logical_value | signal_encoding_physical_value;
signal_encoding_logical_value: 'logical_value' ',' ldf_int ',' ldf_str SEMI;
signal_encoding_physical_value: 'physical_value' ',' ldf_int ',' ldf_int ',' ldf_float ',' ldf_float ',' ldf_str SEMI;

// Signal_representation
signal_representation: 'Signal_representation' '{' signal_representation_definition* '}';
signal_representation_definition: ldf_name ':' ldf_name (',' ldf_name)* SEMI;

// Ldf Elements
ldf_name: CNAME;
ldf_int: C_INT;
ldf_float: C_FLOAT | C_INT;
ldf_version: LIN_VERSION | ISO_VERSION | J2602_VERSION;
ldf_str: ESCAPED_STRING | ldf_version;

// Symbols
EQ: '=';
SEMI: ';';

// Tokens
C_INT: ('0x'HEXDIGIT+)|('-'?INT);
C_FLOAT: ('-'? INT ('.' INT)?) (('e' | 'E')('+' | '-')? INT)?;

// ldf
CNAME: [A-Za-z_][A-Za-z0-9_]*;
LIN_VERSION: '"' INT '.' INT '"' ;
ISO_VERSION: '"' 'ISO17987' ':' INT '"' ;
J2602_VERSION: '"' 'J2602' '_' INT '_' INT '.' INT '"';
ESCAPED_STRING: '"' ('\\' . | ~["\\])* '"';

// Basic Tokens
INT: [0-9]+;
HEXDIGIT:[0-9a-fA-F];

// Ignore
WS: [ \t\r\n]+ -> skip;
CPP_COMMENT: '//' ~[\r\n]* -> skip;
C_COMMENT: '/*' .*? '*/' -> skip;