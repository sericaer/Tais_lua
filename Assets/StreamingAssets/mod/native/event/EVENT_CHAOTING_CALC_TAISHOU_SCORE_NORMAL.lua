EVENT_DEF.GLOBAL.EVENT_CHAOTING_CALC_TAISHOU_SCORE_NORMAL = 
{
    options = 
    {
        OPTION_1=
        {
            selected = function ()
            end,

            next_event = function ()
                if gm_is_occur(0.5) then
                    return 'EVENT_CHAOTING_POWER_PARTY_DISAGREE_SCORE_NORMAL'
                end
                return 'EVENT_CHAOTING_CALC_TAISHOU_SCORE_NORMAL_PUBLISH'             
            end
        }
    }
}