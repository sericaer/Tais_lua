options = 
{
    OPTION_1=
    {
        selected = function ()
        end,

        next_event = function ()
            if gm_data().chaoting.power_party.background == gm_data().taishou.background then
                return 'CHAOTING_CALC_TAISHOU_SCORE_GOOD_PUBLISH'
            end
            if gm_data().chaoting.power_party.background ~= gm_data().taishou.background then
                return 'CHAOTING_CALC_TAISHOU_SCORE_BAD_PUBLISH'
            end
        end
    }
}