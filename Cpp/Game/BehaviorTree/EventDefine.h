#ifndef BEHAVIORTREE_EVENTDEFINE_H
#define BEHAVIORTREE_EVENTDEFINE_H

namespace Egametang {

enum EventType
{
	ON_SPELL_START     = 0,
	ON_SPELL_FINISH    = 1,
	ON_ADD_BUFF        = 2,
	ON_REMOVE_BUFF     = 3,
	ON_HITTED          = 4,
	ON_HIT             = 5
};

enum NodeType
{
	SEQUENCE           = 1,
	SELECTOR           = 2,

	NOT                = 11,

	BUFF_TYPE          = 101,

	CHANGE_HEALTH      = 1001
};

enum SpellUnit
{
	CASTER = 0,
	VICTIM = 1
};

}  // namespace Egametang

#endif // BEHAVIORTREE_EVENTDEFINE_H
