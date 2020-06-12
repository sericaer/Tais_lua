EVENT_DEF.GLOBAL.EVENT_CHAOTING_CALC_TAISHOU_SCORE_BAD_PUBLISH = 
{
    hide = true,

    options = 
    {
        OPTION_1=
        {
            selected = function ()
                gm_data().taishou.year_score = 'BAD'
            end,

            next_event = function ()
                if gm_data().chaoting.power_party.background ~= gm_data().taishou.background then
                    if gm_is_occur(0.3) then
                        return 'EVENT_CHAOTING_REVOKE_TAISHOU_PUBLISH'
                    end
                end
                return 'EVENT_CHAOTING_PIPING_TAISHOU_PUBLISH'
            end
        }
    }
}