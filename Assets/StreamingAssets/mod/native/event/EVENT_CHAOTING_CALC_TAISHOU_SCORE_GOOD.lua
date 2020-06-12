EVENT_DEF.GLOBAL.EVENT_CHAOTING_CALC_TAISHOU_SCORE_GOOD = 
{
    options = 
    {
        OPTION_1=
        {
            selected = function ()
            end,

            next_event = function ()
                if gm_data().chaoting.power_party.background ~= gm_data().taishou.background then
                    if gm_is_occur(0.8) then
                        return 'EVENT_CHAOTING_POWER_PARTY_DISAGREE_SCORE_GOOD'
                    end
                    return 'EVENT_CHAOTING_CALC_TAISHOU_SCORE_GOOD_PUBLISH'
                end               
            end
        }
    }
}