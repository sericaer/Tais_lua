EVENT_DEF.GLOBAL.EVENT_CHAOTING_PIPING_TAISHOU_PLAN = 
{
    occur_rate = function ()
        if gm_data().date.month == 12 and gm_data().date.day == 20 then
            local rate = 0
            if gm_data().chaoting.year_report_tax <  gm_data().chaoting.year_expect_tax then
                rate = rate + 0.4
            end
            if gm_data().chaoting.year_report_pop <  gm_data().chaoting.pre_report_pop then
                rate = rate + 0.6
            end
        end
        return 0
    end,

    options = 
    {
        OPTION_1=
        {
            selected = function ()
            end,

            next_event = function ()
                if gm_data().chaoting.power_pary.background == gm_data().taishou.background then
                    if gm_is_occur(0.3) then
                        return 'EVENT_CHAOTING_POWER_PARTY_DISAGREE_PIPING'
                    end
                    return 'EVENT_CHAOTING_PIPING_TAISHOU_PUBLISH'
                end
            end
        }
    }
}